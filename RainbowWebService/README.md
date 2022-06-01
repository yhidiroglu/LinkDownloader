# RainbowWebService Documentation

## What is the RainbowWebService?
It is a web API project where we can convert weburl to deeplink or vice versa. The project can run by itself, it has a swagger UI and it is easy to use.

## Project Structure
You can see the list of controllers below. These controllers (and endpoints) are documented in swaggerUI.

#### Controllers
1. LinkConvertController

**Technologies Used:**
* C#
* .Net Core WebApi
* Microsoft Access Database
* Serilog
* .Net Core 3.1
* Swashbuckle (Swagger)
* Linq

### Services and Interfaces
You can see all services in the project.

#### LinkConvertService
This service manages to converting processes.

### Helpers
These helpers make our code writing much better.

#### ConfigurationHelper
This helper helps to read properties from appsettings.json.

#### ConvertHelper
This helper converts inputs to target format.

#### DatabaseHelper
This helper connects database.
 
## Environment Setup
Nothing to need anything.

## Project Environments
1. Local (IIS)

## Publish Web API
This project is not ready to publish.

## How To Debug
1. Build RainbowWebService project.
1. Start on IIS.

## Where Are The Logs Stored?
1. We stored the logs in text files with using Serilog. You can find them in Logs folder under project directory as separated by day.
1. Also, we stored API requests and responses in Access Database which is located in Database folder under project directory. Database name is Zeusdb and table name is RainbowServiceRequest.

### How Do I Build RMS?
1. Download project from [gitlab]
1. Check out the `master` branch.
1. Open the solution with the `Visual Studio 2019 or above` version. Compilation of the solution with lower versions of Visual Studio may lead to errors.
1. Build the solution.

### Possible Exceptions
1. In this project, we are using Access Database with Microsoft.Jet.OLEDB.4.0 connection. 
1. So you should not use the project with 64 bit CPU, you need to set the CPU as x86.
1. Otherwise, project cannot log the requests and responses.