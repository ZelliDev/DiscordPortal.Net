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
    /// <see cref="PortalClient"/> Est le client principal pour exécuter des actions sur le Discord Developer Portal
    /// </summary>
    public class PortalClient
    {
        public static string Token;
        public void Connect(string token)
        {
            Token = token;
        }
         

        public void RegeneratingBotToken(Bot bot)
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v6/applications/"+bot.id.ToString()+"/bot/reset");
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


        public Bot CreateBot(string Name)
        {
           Apps app = CreateApplication(Name);
           Bot bot = AddBotToApplication(app);
            return bot;

        }

        public Bot AddBotToApplication(Apps app)
        {
            if (app.bot == null)
            {
                var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v6/applications/" + app.id.ToString() + "/bot");

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

        public void KickUserFromTeam(TeamInformation teaminfo,User team_member)
        {
            HttpWebRequest Req = WebRequest.CreateHttp($"https://discord.com/api/v6/teams/"+teaminfo.id.ToString()+"/members/"+team_member.id.ToString());
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
        public void InviteToTeam(TeamInformation teaminfo, string user, string discriminator)
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v6/teams/"+teaminfo.id.ToString()+"/members");

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

        
        public Apps CreateApplication(string Application_Name)
        {
            
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v6/applications");

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
        public TeamInformation CreateTeamAndGetInformation(string Team_Name)
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v6/teams");

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
        public void CreateTeam(string Team_Name)
        {

            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v6/teams");

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


        public Teams GetTeams()
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v6/teams");
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

        public TeamMembers GetTeamMembers(TeamInformation Team)
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v6/teams/"+Team.id.ToString()+"/members");
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
        

        public Applications GetApplications()
        {
            var Req = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v6/applications?with_team_applications=true");
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
