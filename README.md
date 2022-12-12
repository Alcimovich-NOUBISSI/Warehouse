# Store
This is a warehouse management web app retrieving data from local mysql server. <br/>
You can go through the app, and perform CRUD and filtering operations on: products, categories and providers. <br/>


## Stack Used

    -Asp.net core 6 (MVC) -Bootstrap -Linq -Sql -C# 
    
## Prerequisites
You must make sure you are using the correct version of .net:

     Use asp.net core 6
     
## Run the project 
Before you run the project, first step is to install all NuGet necessary packages: <br/>

    * Microsoft.EntityFrameworkCore.SqlServer
    * Microsoft.EntityFrameworkCore.Tools
    * Microsoft.VisualStudio.Web.CodeGeneration.Design
    
After all packages are installed, make sure you change the connection string in the "appsettings.json" file by putting your server's name :

    "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-Q9E4ALI; Database=Store; User ID= ; Password=; Trusted_Connection=true; encrypt=false "
     }
     
Finally, add migration and update the database by running both commands in the package manager console: 

     Add-Migration -m "[migration name]
     Update-Database
     
You are all set up and ready to start 
    
## Project Demo 

![countriesapi-mobile](https://user-images.githubusercontent.com/80033137/207076767-cc5d3c72-84f9-439c-9298-ff9106b98811.gif)


