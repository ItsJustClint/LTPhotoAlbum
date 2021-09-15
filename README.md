# LTPhotoAlbum (.NET Core 3.1)
Clint Morse - September 14th, 2021

Building and running the application:
  - Copy appsettings.json from project folder to debug or release folder.  This would ideally be handled in the pipeline yaml file during build.
  - Navigate to LTPhotoAlbum project directory:
    - ```cd LTPhotoAlbum```
  - Use dotnet to build the project:
    - ```dotnet build```
  - Navigate to the debug build directory:
    - ```cd bin\Debug\netcoreapp3.1```
  - Run LTPhotoAlbum.exe with no command line arguements:
    - ```LTPhotoAlbum.exe```
  - Once the console has opened, follow instructions listed in the application.  Menu options are listed below and can be accessed at any time:
    - home - Navigate back to launch display
    - album - Display photo albums
    - photo - Search for photos in selected album
    - help - Display these options at any time
    - exit - Closes application

Building and running included unit tests:
  - Copy appsettings.json from test project folder to debug or release folder.  This would ideally be handled in the pipeline yaml file during build.
  - Navigate to the LTPhotoAlbums.Test project directory:
    - ```cd LTPhotoAlbum.Test```
  - Use dotnet to build the project:
    - ```dotnet build```
  - Run the LTPhotoAlbum unit tests:
    - ```dotnet test```
