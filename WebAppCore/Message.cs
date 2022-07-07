using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCore
{
    public class Message
    {

        //        {
        //  "topic": "events",
        //  "ref": null,
        //  "payload": {
        //    "machine_id": "59d9f4b4-018f-43d8-92d0-c51de7d987e5",
        //    "id": "41bb0908-15ba-4039-8c4f-8b7b99260eb2",
        //    "timestamp": "2017-04-16T19:42:26.542614Z",
        //    "status": "running"
        //  },
        //  "event": "new"
        //}

        public string topic { get; set; }
        public string refstr{get;set;}
        public string eventstr { get;set; }
        public payload payloadmodel { get; set; }
        

    }

    public class payload
    {
        public string machine_id { get; set; }
        public string id { get; set; }
        public string timestamp { get; set; }
        public string status { get; set; }
    }

}
