# Overview of SimpleChatApp
This is a real-time online chat application built
with Angular, ASP.NET Core, SQLServer as well as SignalR has been
used for real time chat functionality.
The application is built following scaleable Architecture
with the following functionalities:
  * Register a new account with email address, first name and last name
  * Login using registered email address.
  * Once logged in, users can see the available user to chat. 
    Chats are done end-to-end between two users
  * Users are able to see previous message history.
  * User can also delete the message
  * Users can sign out whenever they decide
  
 # Technologies
  * ASP.NET Core 3.1
  * Angular 11
  * Microsoft SQL Server
  * Entity Framwork Core
  * SignalR
  * Bootstarp 4
  
 # Getting Started
 This following application/tools need to be install in computer as prerequisite
  
 ## Prerequisites
  1. Install [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) (skip if already installed)
  2. Install [Node.js LTS](https://nodejs.org/en/) (skip if already installed)
  3. Install [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)  (skip if already installed)
  4. Install [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)  (skip if already installed)
  
  
  ## Setup Appication
  Clone the repository with 'git clone https://github.com/saykat/SimpleChatApp.git' command if git cli is installed or download project as zip from 'https://github.com/saykat/SimpleChatApp'
  
  ### Run DOT.NET Core Application
   1. Open project folder and move to ChatApi directory then click on ChatApi.sln to open the project with visual studio.
   2. Open appsettings.json and configure it if it is needed. Usally do not neet to change the database if the same database does not exist <br>
      "ConnectionStrings": {<br>
        "Default": "Server=<span style='color:green'>(local)</span>;Database=ChatAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"<br>
      } 
   3. Run 'update-database' from package maneger console to migrate database.
   4. After migration is done, Run the project with visual studio and project will be run on localhost port:44385.
   
   ### Run Angular Application
   1. Open project folder and move to ChatClient directory and click shift and mouse right button together then open from here power-shell/command-prompt.
   2. Run 'npm install' to install all the node packages for the angular appication.
   3. Run 'npm start' command to run the application on 'http://localhost:4200'
