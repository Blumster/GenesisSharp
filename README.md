# GenesisSharp

Estimated completion: 5%

Needs implementation:
 - Movement synchronization
 - Damage system
 - Skill system
 - Convoy system
 - Clan system
 - Spawn system
 - Map transfer system
 - Chat system
 - Quest system
 - Looking For Group system
 - AI Scripting system

TNL.NET and UniversalAuth libraries must be put into the Libraries folder. You have to clone and compile them separately.

It is only compatible with the v14.117 client!

Disclaimer: It might be against the law to run this application in certain countries. Use at your own risk.

How to compile:
- Install Visual Studio 2013 (Community edition is free)
- Open UniversalAuth in VS2013 (https://github.com/Blumster/UniversalAuth)
- Compile it (keep in mind: if you intend to build the server in debug mode, build this in debug mode too!) and move the generated file (UniversalAuth.dll and .pdb) to the Libraries folder from the UniversalAuth\UniversalAuth\bin\Debug or Release folders
- Close UnversalAuth and open TNL.NET (https://github.com/Blumster/TNL.NET) in VS2013
- Compile it and move the files, just like you did with the previous library
- Close TNL.NET and open GenesisSharp project file in VS2013
- Compilation should succeed

Setting up the database:
- You have to set up an ordinary MySQL server on your computer, or rent one or anyhow you can get a MySQL server.
- There is an SQL named folder in the repo. It's a clean dump of the database. You have to run it against a database, which will contain the game data
- You have to change a few things in the database.
- Edit the only row in realmlist_global table. Change the address to your lan (!!!) address! (Lan adresses start with 192.168.\*\*\*.\*\*\*)
- Edit the only row in realmlist_sector table. Change the address to your lan (!!!) address!
- Add a new row to the account table. You can set the Id to NULL (auto-generated), but you have to specify a Username and Password (both plain text). The other columns are not used yet, or auto filled with data.

How to run:
- If the compilation succeeded, you will have 3 executables at your hand. One in Genesis.Auth (Authentication), one in Genesis.Global (Global server) and the lost one in Genesis.Sector. (Sector server)
- Next to each of them there is a config file. (Genesis.Auth.exe.config, Genesis.Global.exe.config and Genesis.Sector.exe.config)
- The ConnectionString value in all 3 must be the same. This ConnectionString must point to the server you've set up before. You have to write it once and you can copy over to the other config files. I'd recommend not to change anything else than the ConnectionString in the Auth config.
- The Global and Sector config contains an AssetPath value, which must be set to a v14.117 client folder. For example: if you install it to C:\AA, you have to put "C:\AA\" to the value. I'd recommend not to change anything else.
- If all three config files are edited, the next step is to redirect the client to connect to your computer.
- You will find a vog.ini file in the exe folder, where you have to change the AUTHSERVERIP parameter to your lan (!!!) address.
- If you did all that, you should be safe to launch Genesis.Auth.exe, and when it started, launch Genesis.Global.exe and if that started up too (it stated, that listening to connections) start Genesis.Sector.exe
- Create a shortcut to the game executable to your desktop. If that is done, edit the shortcut, and put the keyword "-developer" behind the executable's path (without the " characters)
- Run the client, and log in with your previously created user
- Have fun!

I have no right to distribute the client. For more info about that, check the apokalypsos forums
(http://apokalypsos.com/forum/viewtopic.php?f=10&t=89)
