using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPortal.Net
{
    /// <summary>
    /// <see cref="PortalClient"/> is the main client to used in the library
    /// </summary>
    public class PortalClient
    {
        public static string Token;
        /// <summary>
        /// <see cref="Connect(string)"/> is the main connecting method used by the client
        /// </summary>
        /// <param name="token"></param>
        public void Connect(string token)
        {
            Token = token;
        }
         

        /// <summary>
        /// <see cref="AddAppWhitelist(Apps, string, string)"/> is used to invite users to app WhiteList
        /// </summary>
        /// <param name="app"></param>
        /// <param name="username"></param>
        /// <param name="discriminator"></param>
        public void AddAppWhitelist(Apps app, string username, string discriminator)
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/oauth2/applications/"+app.id.ToString()+"/whitelist");
            var postData = "{⍚username⍚:⍚"+username+ "⍚,⍚discriminator⍚:⍚"+discriminator+"⍚}";

            var data = Encoding.ASCII.GetBytes(postData.Replace('⍚', '"'));
            string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36";
            Req.Method = "POST";
            Req.UserAgent = UserAgent;
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);
            Req.ContentLength = data.Length;

            using (var stream = Req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)Req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        /// <summary>
        /// <see cref="RegeneratingBotToken(Bot)"/> is used to regenarate bot Token's
        /// </summary>
        /// <param name="bot"></param>
        public void RegeneratingBotToken(Bot bot)
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/applications/"+bot.id.ToString()+"/bot/reset");
            var postData = "{}";

            var data = Encoding.ASCII.GetBytes(postData.Replace('⍚', '"'));
            string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36";
            Req.Method = "POST";
            Req.UserAgent = UserAgent;
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);
            Req.ContentLength = data.Length;

            using (var stream = Req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)Req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        /// <summary>
        /// <see cref="GetApplicationByName(string)"/> return the class <see cref="Apps"/> from the name (string) given
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Apps GetApplicationByName(string name)
        {
            Applications applications = GetApplications();
            foreach (Apps app in applications.Apps)
            {
                if (app.name == name)
                {
                    return app;
                }

            }
            return null;
        }

        /// <summary>
        /// <see cref="CreateBot(string)"/> Create an application and activate bot on it, return <see cref="Apps"/> from the name (string) given
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Bot CreateBot(string Name)
        {
           Apps app = CreateApplication(Name);
           Bot bot = AddBotToApplication(app);
            return bot;

        }

        /// <summary>
        /// <see cref="AddBotToApplication(Apps)"/> Activate the bot instance to an application that does not have bot already activated
        /// <para></para>
        /// return the class <see cref="Bot"/> from the <see cref="Apps"/> given
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public Bot AddBotToApplication(Apps app)
        {
            if (app.bot == null)
            {
                var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/applications/" + app.id.ToString() + "/bot");

                var postData = "{}";

                var data = Encoding.ASCII.GetBytes(postData.Replace('⍚', '"'));
                string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36";
                Req.Method = "POST";
                Req.UserAgent = UserAgent;
                Req.ContentType = "application/json";
                Req.Headers.Add("authorization", Token);
                Req.ContentLength = data.Length;

                using (var stream = Req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)Req.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Bot bot = Newtonsoft.Json.JsonConvert.DeserializeObject<Bot>(responseString);
                return bot;
            }
            else
            {
                return null;
            }
           
        }
        /// <summary>
        /// <see cref="KickUserFromTeam(TeamInformation, User)"/> Kick a user from a selected team
        /// </summary>
        /// <param name="teaminfo"></param>
        /// <param name="team_member"></param>
        public void KickUserFromTeam(TeamInformation teaminfo,User team_member)
        {
            HttpWebRequest Req = WebRequest.CreateHttp($"https://discord.com/api/v8/teams/"+teaminfo.id.ToString()+"/members/"+team_member.id.ToString());
            string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36";
            Req.Method = "DELETE";
            Req.UserAgent = UserAgent;
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);

            using (Stream ReqResponseStream = Req.GetResponse().GetResponseStream())
            {
                using (StreamReader ReqResponse = new StreamReader(ReqResponseStream))
                {
                    string Resp = ReqResponse.ReadToEnd();
                    ReqResponse.Close();
                   
                }
            }

        }
        /// <summary>
        /// <see cref="InviteToTeam(TeamInformation, string, string)"/> invite user to selected team
        /// </summary>
        /// <param name="teaminfo"></param>
        /// <param name="user"></param>
        /// <param name="discriminator"></param>
        public void InviteToTeam(TeamInformation teaminfo, string user, string discriminator)
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/teams/"+teaminfo.id.ToString()+"/members");

            var postData = "{username: ⍚" + user+ "⍚, discriminator: ⍚" + discriminator+ "⍚}";

            var data = Encoding.ASCII.GetBytes(postData.Replace('⍚', '"'));
            string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36";
            Req.Method = "POST";
            Req.UserAgent = UserAgent;
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);
            Req.ContentLength = data.Length;

            using (var stream = Req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)Req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        }

        /// <summary>
        /// <see cref="CreateApplication(string)"/> Create an application with the name given and return <see cref="Apps"/> class
        /// </summary>
        /// <param name="Application_Name"></param>
        /// <returns></returns>
        public Apps CreateApplication(string Application_Name)
        {
            
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/applications");

            var postData = "{*name*: *"+Application_Name+"*, *team_id*: null}";

            var data = Encoding.ASCII.GetBytes(postData.Replace('*','"'));
            string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36";
            Req.Method = "POST";
            Req.UserAgent = UserAgent;
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);
            Req.ContentLength = data.Length;

            using (var stream = Req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)Req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Apps app = Newtonsoft.Json.JsonConvert.DeserializeObject<Apps>(responseString);
            return app;
        }

        /// <summary>
        /// <see cref="CreateTeamAndGetInformation(string)"/> Create a team with the name given and return <see cref="TeamInformation"/> class
        /// </summary>
        /// <param name="Team_Name"></param>
        /// <returns></returns>
        public TeamInformation CreateTeamAndGetInformation(string Team_Name)
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/teams");

            var postData = "{" + $"\"name\":\"{Team_Name}\"" + "}";

            var data = Encoding.ASCII.GetBytes(postData);
            string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36";
            Req.Method = "POST";
            Req.UserAgent = UserAgent;
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);
            Req.ContentLength = data.Length;

            using (var stream = Req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)Req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            TeamInformation team = Newtonsoft.Json.JsonConvert.DeserializeObject<TeamInformation>(responseString);
            return team;
           
        }

        /// <summary>
        /// Create a team with the name given
        /// </summary>
        /// <param name="Team_Name"></param>
        public void CreateTeam(string Team_Name)
        {

            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/teams");

            var postData = "{" + $"\"name\":\"{Team_Name}\"" + "}";

            var data = Encoding.ASCII.GetBytes(postData);
            string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36";
            Req.Method = "POST";
            Req.UserAgent = UserAgent;
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);
            Req.ContentLength = data.Length;

            using (var stream = Req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)Req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Dispose();
            response.Close();
            

        }

        /// <summary>
        /// <see cref="GetTeams"/> Get all teams and return the class <see cref="Teams"/>
        /// </summary>
        /// <returns></returns>
        public Teams GetTeams()
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/teams");
            Req.Method = "GET";
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);
            var response = (HttpWebResponse)Req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string raw = "{*TeamInformation*:";
            Teams team = JsonConvert.DeserializeObject<Teams>(raw.Replace('*', '"') + responseString+"}");
            response.Dispose();
            response.Close();

            
            return team;
        }

        /// <summary>
        /// <see cref="GetTeamMembers(TeamInformation)"/> Get the teams members and return the class <see cref="TeamMembers"/>
        /// </summary>
        /// <param name="Team"></param>
        /// <returns></returns>
        public TeamMembers GetTeamMembers(TeamInformation Team)
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/teams/"+Team.id.ToString()+"/members");
            Req.Method = "GET";
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);
            var response = (HttpWebResponse)Req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string raw = "{*TeamMember*:";
            TeamMembers team = JsonConvert.DeserializeObject<TeamMembers>(raw.Replace('*', '"') + responseString + "}");
            response.Dispose();
            response.Close();


            return team;
        }
        
        /// <summary>
        /// <see cref="GetApplications"/> Get All Applications and Return the class <see cref="Applications"/>
        /// </summary>
        /// <returns></returns>
        public Applications GetApplications()
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v8/applications?with_team_applications=true");
            Req.Method = "GET";
            Req.ContentType = "application/json";
            Req.Headers.Add("authorization", Token);
            var response = (HttpWebResponse)Req.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string raw = "{*Apps*:";
            Applications applications = JsonConvert.DeserializeObject<Applications>(raw.Replace('*','"')+responseString+"}");
           
            response.Dispose();
            response.Close();

            return applications;

        }
    }
}
