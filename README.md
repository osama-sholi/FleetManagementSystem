# Fleet Management System
Click on the image below to watch the overview video of Fleet Management System
[![Fleet Management System Frontend Overview Video](https://i.ibb.co/MGxyc9w/Screenshot-1.png)](https://youtu.be/JSKHr_S_1zI)

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

### Run the application
Just run the start.bat file and that's it.
What does the start.bat file do?
* It runs the run.bat in the backend folder and the serve.bat in the frontend folder.
* run.bat file checks and install the required packages for the backend and then run the backend server.
* serve.bat file checks and install the required packages for the frontend and then run the frontend application.

## Postman Collection
You can find the postman collection in the project folder. it's called 'osama_fms.postman_collection.json'.
You can import it to your postman and test the APIs.