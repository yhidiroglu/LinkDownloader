# RainbowWebService Documentation

## What is the RainbowWebService?
It is a web API project where we can download webpages with sending url.

## Project Structure
You can see the list of controllers below. These controllers (and endpoints) are documented in swaggerUI.

#### Controllers
1. LinkDownloadController

**Technologies Used:**
* C#
* .Net Core WebApi
* Serilog
* .Net Core 3.1
* Swashbuckle (Swagger)
* Linq
* Monolith
* Regex

### Services and Interfaces
You can see all services in the project.

#### LinkConvertService
This service manages to download processes.

### Helpers
These helpers make our code writing much better.

#### ConfigurationHelper
This helper helps to read properties from appsettings.json.

#### DownloadHelper
This helper downloads inputs to target format.

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

### How Do I Build RMS?
1. Download project from [gitlab]
1. Check out the `master` branch.
1. Open the solution with the `Visual Studio 2019 or above` version. Compilation of the solution with lower versions of Visual Studio may lead to errors.
1. Build the solution.

### Possible Exceptions
1. So you should not use the project with 64 bit CPU, you need to set the CPU as x86.
1. Otherwise, project cannot log the requests and responses.