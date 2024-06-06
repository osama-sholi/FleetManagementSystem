# Fleet Management System
Click on the image below to watch the overview video of Fleet Management System
[![Fleet Management System Frontend Overview Video](https://i.ibb.co/MGxyc9w/Screenshot-1.png)](https://www.youtube.com/watch?v=c_v0NvKS-Hk)

## How to run the project

### Download the project
Download the project by clicking on the green "Code" button and then click on "Download ZIP"
then extract the downloaded zip file.

### Set up the database
There is a file called 'osama_fms_dumpfile.sql' in the project folder. it's a dump file of the database.
Restore it either by using the command line:

```bash
mysql -u username -p database_name < osama_fms_dumpfile.sql
```
or by using phpMyAdmin:
1. Create a new database.
2. Select the newly created database.
3. Click on the "Import" tab.
4. Choose the dump file and click on "Go".

### Prerequisites
Before you can run this application, you need to install the following:

1. **.NET Core SDK**: This is required to run the .NET Core backend. You can download it from the [.NET Core official website](https://dotnet.microsoft.com/download). Please install the version that matches the one specified in the project file.

2. **Node.js and npm**: These are required to install the necessary packages for the Angular frontend and to serve the frontend application. You can download Node.js, which includes npm, from the [Node.js official website](https://nodejs.org/en/download/).

3. **Angular CLI**: This is required to serve the Angular frontend. After installing Node.js and npm, you can install Angular CLI globally by running the following command in your terminal:

```bash
npm install -g @angular/cli
```

### Run the application
Just run the start.bat file and that's it.
What does the start.bat file do?
* It runs the run.bat in the backend folder and the serve.bat in the frontend folder.
* run.bat file checks and install the required packages for the backend and then run the backend server.
* serve.bat file checks and installs the required packages for the frontend and then run the frontend application.

## Postman Collection
You can find the Postman collection in the project folder. it's called 'osama_fms.postman_collection.json'.
You can import it to your postman and test the APIs.

## Web Socket
Since there is no frontend implementation for the web socket, and for some reason I can't export it using postman, here are the steps to test it out.
1. Open Postman.
2. Right-click the collection and add the WebSocket request
3. Use this link "ws://localhost:5179/api/ws", you have to replace the domain if it's different on your side, and then click connect.
4. Create a new WebSocket with the same link and connect to the server.
5. Using one of the WebSockets send a GVAR JSON, something like this:
```bash
{
    "DicOfDic": {
        "Tags": {
            "VehicleID" : "2",
            "VehicleDirection": "123",
            "Status" : "1",
            "VehicleSpeed" : "123",
            "RecordTime": "5123121",
            "Address":"ada",
            "Latitude": "21123",
            "Longitude": "123"
        }
    },
    "DicOfDt":{}
}
```
6. Click send and check the other WebSocket connection, it should receive an acknowledgment GVAR object.
