# Tigerspike Technical Challenge

Thanks for taking the time to review this project as part of the application process. I tackled the .NET version of the challenge and after getting approval from Matt Whelan, I chose React Native as the front end as I wanted to test out using Expo (previous RN projects I've done have required Android Studio or XCode to build / test)

## Build Instructions

 - Unzip the package
 - Open `src\API\TigerSpike.LandmarkRemark.API.sln` in Visual Studio
 - Run `Update-Database` against the `TigerSpike.LandmarkRemark.Data` project in the NuGet Package Manager Console to create the database
 - Get your local IP address from the command line and change all references to `192.168.0.150` in `src\API\TigerSpike.LandmarkRemark.API\Properties\launchSettings.json` to be your IP. Make the same change in `src\App\services\api.ts`
 - Start the API in IISExpress by clicking Play for the `TigerSpike.LandmarkRemark.API` profile. Leave the launched browser window open to keep IISExpress running
 - Add a firewall rule to allow LAN traffic to access IISExpress
 - Install the Expo app on your device
 - At the command line, run `npm install` in the `src\App` folder
 - At the command line, run `npm run web` in the `src\App` folder. Two sites will launch, the first is the Expo UI, the second is the web version of the Expo project. Close the web version as that fails given the Maps component used is not a web version. What you want is to scan the QR Code in the Expo UI using the Expo app on your phone - this will launch the app on your device without cables needed. Please note, I have not tested on an iOS device as I don't own one or a Mac for emulation
 
## Time Spent

This is definitely the longest I've spent on a tech test as I spent roughly 14 hours in total. As I mentioned to Ella, I was only able to begin this on Monday and had to tackle it after my daughter went to bed

 - Project Setup - 1 hr
 - API - 4 hrs
 - API Tests - 2 hrs
 - React Native App - 6 hrs
 - Documentation & Handover - 1 hr
 
## Notes
- There is no user management as I was time constrained. A set of users is pre-loaded into the database instead
- The UI is very basic. In particular, the user selector is very rudimentary as you can only cycle through users already in the database (didn't want to invest too long on this as it wasn't a core requirement)
- I have not tested on an iOS device as I don't own one or a Mac for emulation
 