# consultants
This is a full stack solution with Angular frontend, APIs and Database

The solution is made as a job application test for Flextribe.

Parts:

frontend - the Angular SPA

backend/api - .NET Core API

backend/database - the set up of the SQL database


Instructions for trying out the backend:

1. Import the database to your local SQL Server
2. Go to /backend/api/webapi folder and open the webapi.sln
3. Go to the appsettings.json file and update the "ConnectionStrings.ConsultantApiConnection" so it matches your own database
4. Build the solution
5. Run the application
6. Open Postman and go to http://localhost:5000/api/Consultant/{id}
	Where 'id' is the id of the Consultant
	(Check your localhost port, you might need to adjust it)




