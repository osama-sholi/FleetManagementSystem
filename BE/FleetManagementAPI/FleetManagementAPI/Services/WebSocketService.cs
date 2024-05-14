using FPro;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace FleetManagementAPI.Services
{
    public class WebSocketService
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public WebSocketService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        // Handle incoming WebSocket connections
        public async Task HandleConnection(HttpContext context)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            string socketId = AddSocket(webSocket);

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var routeHistoryService = scope.ServiceProvider.GetRequiredService<RouteHistoryService>();

                    await ReceiveMessage(webSocket, async (result, buffer) =>
                    {
                        // If the message type is Text, deserialize the message and add it to the route history
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                            GVAR? gvar = JsonSerializer.Deserialize<GVAR>(message);

                            if (gvar == null)
                            {
                                throw new Exception("GVAR object is null");
                            }

                            routeHistoryService.AddRouteHistory(gvar);

                            GVAR gvarMessage = new GVAR();
                            gvarMessage.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                            gvarMessage.DicOfDic["Tags"]["STS"] = "1";
                            await SendMessageToAllAsync(gvarMessage);
                        }

                        // Close the connection if the message type is Close
                        else if (result.MessageType == WebSocketMessageType.Close)
                        {
                            string id = GetId(webSocket);
                            await RemoveSocket(GetId(webSocket));
                        }

                    });
                }
            }
            // If an exception is thrown, send a message to the client with the status code 0
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                GVAR gvar = new GVAR();
                gvar.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
                gvar.DicOfDic["Tags"]["STS"] = "0";
                String json = JsonSerializer.Serialize(gvar);
                var buffer = Encoding.UTF8.GetBytes(json);
                if (webSocket.State == WebSocketState.Open)
                {
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).ConfigureAwait(false);
                }
            }
            // Close the connection
            finally
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    await RemoveSocket(socketId).ConfigureAwait(false);
                }
            }
        }

        // Get a WebSocket connection by ID
        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        // Get all WebSocket connections
        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }

        // Get the ID of a WebSocket connection
        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }


        // Add a WebSocket connection
        public string AddSocket(WebSocket socket)
        {
            string connId = Guid.NewGuid().ToString();
            _sockets.TryAdd(connId, socket);
            return connId;
        }

        // Receive a message from a WebSocket connection
        public async Task ReceiveMessage(WebSocket socket, Func<WebSocketReceiveResult, byte[], Task> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);
                await handleMessage(result, buffer);
            }
        }

        // Send a message to all WebSocket connections
        public async Task SendMessageToAllAsync(GVAR gvar)
        {
            foreach (var pair in _sockets)
            {
                String json = JsonSerializer.Serialize(gvar);
                if (pair.Value.State == WebSocketState.Open)
                    await pair.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        // Remove a WebSocket connection
        public async Task RemoveSocket(string id)
        {
            if (_sockets.TryRemove(id, out var socket) && socket != null)
                await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                        statusDescription: "Closed by the WebSocketManager",
                                        cancellationToken: CancellationToken.None);
        }
    }
}