# AzureBlobStorage
This project enables users to upload to and download from an Azure Blob Container using C#, .Net Core 6, Entity Framwork Core code first, In Memory Database, MediateR, AutoMapper and Swagger.

To get started:
Open 'Package Manager Console' in visual studio and type in the following commands to create the database(Read the NOTE: comments in DataContext.cs)
PM> dotnet ef database update
In the WebApi project change the fololowing in the appsettings.json
-> Set the ConnectionString to create the CloudBlobClient
-> Set the FileContentType to the desired type
-> Set the StorageContainer to the desired name
-> Set the BlobName to the desired name
Select WebApi as startup project, select IIS Express and  hit play
Right click on the ConsoleApp -> Debug -> Start New Instance
You should be good to go!
