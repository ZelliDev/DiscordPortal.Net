using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPortal.Net
{
    public class Owner
    {
        public string id;
        public string username;
        public string avatar;
        public string discriminator;
        public int public_flags;
        public int flags;
    }

    public class Bot
    {
        public string id;
        public string username;
        public string avatar;
        public string discriminator;
        public int public_flags;
        public bool bot;
        public string token;
        
    }

    public class Publisher
    {
        public string id;
        public string name;
    }

    public class Developer
    {
        public string id;
        public string name;
    }

    public class Apps
    {
        public string id;
        public string name;
        public string icon;
        public string description;
        public string summary;
        public bool hook;
        public string verify_key;
        public Owner owner;
        public int flags;
        public string secret;
        public List<object> redirect_uris;
        public int rpc_application_state;
        public int store_application_state;
        public int verification_state;
        public bool? bot_public;
        public bool? bot_require_code_grant;
        public Bot bot;
        public string cover_image;
        public string guild_id;
        public string primary_sku_id;
        public string slug;
        public List<Publisher> publishers;
        public List<Developer> developers;
    }

    public class Applications
    {
        public List<Apps> Apps { get; set; }
    }

}
