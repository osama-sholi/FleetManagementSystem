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
                        else if (result.MessageType == WebSocketMessageType.Close)
                        {
                            string id = GetId(webSocket);
                            await RemoveSocket(GetId(webSocket));
                        }

                    });
                }
            }
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
            finally
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    await RemoveSocket(socketId).ConfigureAwait(false);
                }
            }
        }
        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        public string AddSocket(WebSocket socket)
        {
            string connId = Guid.NewGuid().ToString();
            _sockets.TryAdd(connId, socket);
            return connId;
        }
        public async Task ReceiveMessage(WebSocket socket, Func<WebSocketReceiveResult, byte[], Task> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);
                await handleMessage(result, buffer);
            }
        }
        public async Task SendMessageToAllAsync(GVAR gvar)
        {
            foreach (var pair in _sockets)
            {
                String json = JsonSerializer.Serialize(gvar);
                if (pair.Value.State == WebSocketState.Open)
                    await pair.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task RemoveSocket(string id)
        {
            if (_sockets.TryRemove(id, out var socket) && socket != null)
                await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                        statusDescription: "Closed by the WebSocketManager",
                                        cancellationToken: CancellationToken.None);
        }

        private string GetConnectionId() => Guid.NewGuid().ToString();
    }
}