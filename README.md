# WackyArch Emulator Frontend

This is the C# Blazor frontend for the [WackyArch](https://github.com/benjamin-allen/wacky-arch) emulator.

The C# code is all contained in the WackyArchServer Project. The other files at the root of this repo are some of the SQL files I used to set up the challenges in the database.


# Running it yourself
1. You'll need MS Visual Studio, probably. You'll also need to have Blazor development packages downloaded.
2. The project is built in .NET 6, so have that installed.
3. Download/clone the repo. Extract it somewhere.
4. Download the [WackyArch Emulator](https://github.com/benjamin-allen/wacky-arch), which is what actually implements the architecture.
   Extract it somewhere collocated with the WackyArchServer folder.
5. Open the WackyArchServer project. You may see that the WackyArch project couldn't load.
   If so, edit the project reference to point to the location of the WackyArch project on your filesystem.
   Do similarly with the tests package, which is part of the WackyArch repository.
6. Once everything loads, Right-click on the solution and restore nuget packages.
7. You should be able to build and run the project (or the tests), but will get an error related to database access.
8. In Program.cs, change the connection settings to use a database of your choice.
9. In the Package Manager Console, run the command `Update-Database` to apply the EFCore Database Changes
10. If you want to recreate the challenges found at Jolt, use the provided SQL files to run commands against the DB.
11. Run again. Many of you found this already, but use the URL `/identity/account/register` to register a user.


# Known Issues
- Lots of minor crashes in the emulator aren't caught by the frontend and have a default exception handler.
  When this happens you have to just refresh the page.
- Comments in the code can be rejected if they don't match the regex `[a-zA-Z0-9\s@#]`