# Green Flux - Smart Charging System

## Problem to be be implement

![image](https://user-images.githubusercontent.com/11566992/201849059-1ef67ce6-bd38-4634-be0a-174bbe32ef2e.png)

## Technologies used in the project
- .Net 6
- Entity Framework Core
- NUnit
- Auto Mapper
- MOQ
- SQL server

## Project Architecture

![image](https://user-images.githubusercontent.com/11566992/199804705-c3b0f08b-1d2b-4f73-a4a6-e5b2a495cece.png)

- API : Contains All the API exposed to outside
- Application : Contains Service level seperation
- Infrastrcture : Contains repositories
- Domin : Contains core domain objects

## Development Considerations 
- Entities can be added/deleted/updated seperately
- Business logics handles only in the API layer
- When creating a ChargeStation will create a Connector
- ChargeStation cannot change added to a new group (need to delete and create)

## Future Development
- Increase Test Coverage
- Cleanup Test projects
- Business logic / constaints handle seprately
- Add logging

## Run the project

### Pre-requirements

Make sure you have installed the following to run the project:
- .Net 6
- Visual Studio 2022
- SQL server

### Steps

#### Run in Local
- Download/Checkout the code ("Master" branch)
- Open the project from visual studio 2022
- Check the appsettings.json for the connection string and update it to yours(SmartCharging\SmartCharging\appsettings.json)
- Build & Run the project (you should be able to view the open API documentation through Swagger)
