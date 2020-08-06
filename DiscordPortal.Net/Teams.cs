using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPortal.Net
{
    public class TeamInformation
    {
        public string id { get; set; }
        public string icon { get; set; }
        public string name { get; set; }
        public string owner_user_id { get; set; }
        
    }

    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public string discriminator { get; set; }
        public int public_flags { get; set; }
    }

    public class TeamMember
    {
        public User user { get; set; }
        public string team_id { get; set; }
        public int membership_state { get; set; }
        public List<string> permissions { get; set; }
    }

    public class TeamMembers
    {
        public List<TeamMember> TeamMember { get; set; }
    }
    public class Teams
    {
        //public List<TeamMember> TeamMembers { get; set; }
        public List<TeamInformation> TeamInformation { get; set; }
    }

}
