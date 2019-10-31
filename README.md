# Integrator

Application that integrates multiple services. 
[GitHub / Integrator](https://github.com/varitomirandacr/Integrator)

The services are also uploaded in Docker as containers if you would like to download them:
[Docker](https://hub.docker.com/u/varitomiranda)

###### locationservice, networkservice, virusservice and (Main) restintegrator
```bash
docker pull varitomiranda/<<container-name>>
```

### Important
- The services can be tested with Swagger UI too.
- Each service is ran asynchronously.
- The services implement Rate Limit  of 1 every 10 seconds.
- Every Services catches and returns its own errors and Json.


## Usage

### UI
Deployed to Azure a very simple web application. Very basic UI in MVC just to use a visual (nothing to be proud of) user interface.
- [Integrator Web App - UI](https://integrator20191028073408.azurewebsites.net/)

Also, I deployed every service as a unique or single App Service in Azure. However I suggest to go to the Main API which contains a reference for all the services. 


- [Main API - Swagger](https://restintegrator20191029101642.azurewebsites.net/swagger/index.html)
This app integrates all services.

#### Parameters: 
target = "google.com"

services = ["icmp", "dnsresolver", "dnschilkat", "dnslookup", "virusscan"]

##

- [Location Service - Swagger](https://locationservice20191027105615.azurewebsites.net/swagger/index.html)
This Location service contains the GeoIP Location service. 
["geoip"]


- [NetworkService - Swagger](https://networkservice20191026025909.azurewebsites.net/swagger/index.html)
This Network service contains the ICMP(ping), IP resolver, Dns Resolver, Dns through an API called Chilkat and Dns Lcient Lookup through .NET Core libraries. It also contains a GetHostname method that does not get exposed to convert from IP to Hostname.
["icmp", "dnsresolver", "dnschilkat", "dnslookup"]


- [VirusScanService- Swagger](https://virusservices20191026023616.azurewebsites.net/swagger/index.html) This service uses VirusTotal restful service to request data about the status of a website.
["virusscan"]

