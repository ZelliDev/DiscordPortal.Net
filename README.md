# DiscordPortal.Net
> Usefull Library With Simplified Method In relative with the Discord Developers Portal

[![Active](http://img.shields.io/badge/Status-Active-green.svg)](https://tterb.github.io)


Easy Installation and Usage 

## Special Feature
- Create Applications and Bot Directly from the Library
- Get informations about Bot
- Get informations about Teams
- Get Developers liscence informations about Applications (NEW)
- Updated to Discord API V8 (NEW)

## Required
Newtonsoft.Json is Required

## Installation
```

Download the current release and add it to your projects

```

## Usage example
Client Initialisation and Connection
````cs
// Declare a new instance of the class PortalClient
PortalClient client = new PortalClient();

// Read a file where is stored the token (or you can add it directly)
string token = File.ReadAllText("token.txt");

// Connecting on the api with the given token
client.Connect(token);
````

Applications Initialisation and Usage

````cs
// Get all the applications
Applications applications = client.GetApplications();

// Get Application By Name
Apps appli = client.GetApplicationByName("test");
            
// Create An Application
Apps app = client.CreateApplication("zellybottest");
````

Bot Initialisation and Usage

````cs
// Add a bot on an application that does not have a bot already
Bot bot = client.AddBotToApplication(app);

// Create a new application and add a bot on that application
Bot bot = client.CreateBot("NomDuBot");
// Get Bot's Informations by the applications list
Bot bot = applications.Apps[0].bot;

// Regen the bot Token
client.RegeneratingBotToken(bot);
````

Team Initialisation and Usage

````cs
// Create a new team with the chosen name
client.CreateTeam("ZelTeam");
          
// Declare a new class of Teams and get all your teams
Teams team = client.GetTeams();
// You can get team members information by using GetTeamMember(TeamInformation);
TeamMembers members = client.GetTeamMembers(team.TeamInformation[0]);

// Invite the user to the selected team
client.InviteToTeam(team.TeamInformation[0],"Zelly","6666");

// Kick the User from the selected team
client.KickUserFromTeam(team.TeamInformation[0], members.TeamMember[0].user);

````

## Release History
* 1.0.0.0A
    * Added Summary to PortalClient.CS
    * Fixed Assembly CultureInfo Bugs

* 1.0.0.0
    * First Upload Of DiscordPortal.Net






