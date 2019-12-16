# Toxic-Ingredients-Total-Scanner


## Script

Follow these steps to run this api.  <br/>

**Requirements**

1. To run this app you have to add `appsettings.json` file in `/api/TITS_API.Api`. This file should looks like:
```javascript
{
	  "Logging": {
	    "LogLevel": {
	      "Default": "Information",
	      "Microsoft": "Warning",
	      "Microsoft.Hosting.Lifetime": "Information"
	    }
	  },
	  "ConnectionStrings": {
	    "DefaultConnection": "connection string to your database"
	  },
	  "PwSAPI": {
	    "Login": "your email used to sign up to http://produktywsieci.gs1.pl/",
	    "Key": "your api key"
	  },
	  "AllowedHosts": "*"
}
```

**Preparing your Environment**

1. If you use Visual Studio:
   - use Visual Studio Installer to download and install:
	   - .NET Core SDK (3.0 or higher)
	   - NuGet Package manager
	   - NuGet targets and builds tasks
2. If you use any other software:
	- Install the [.NET Core SDK](https://dot.net/core) (3.0 or higher) for your operating system.
	- Git clone this repository: 
		`git clone https://github.com/Toxic-Ingredients-Total-Scanner/Toxic-Ingredients-Total-Scanner.git`
	- Navigate to the local folder on your machine at the command prompt or terminal.
3. Use this command in terminal to migrate models to database:
`update-database`

**Run the application**

3. If you use Visual Studio:
   - select `TITS_API.Api` as startup project and click `Start` button to compile nad run.
4. If you use any other software:
	- Run application: `dotnet run`
	- Alternatively, you can build and directly run your application with the following two commands:
	   - `dotnet build`
	   - `dotnet bin/Debug/netcoreapp3.0/TITS_API.Api.dll`
	- Enter [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html) in your browser to open swagger documentation.