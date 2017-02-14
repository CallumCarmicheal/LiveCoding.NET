using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static LiveEdu.API.Wrappers.Serializer;

namespace LiveEdu.API.Wrappers.Models {

    /**
        JSON Schema:
            ScheduledBroadcastSerializer {
                id                              (integer),
                title                           (string),
                livestream                      (string),
                coding_category                 (string),
                difficulty                      (string),
                start_time                      (string),
                start_time_original_timezone    (string),
                original_timezone               (string),
                is_featured                     (boolean),
                is_recurring                    (boolean)
            }

        JSON Result:
            {
              "id":                             0,
              "title":                          "",
              "livestream":                     "",
              "coding_category":                "",
              "difficulty":                     "",
              "start_time":                     "",
              "start_time_original_timezone":   "",
              "original_timezone":              "",
              "is_featured":                    false,
              "is_recurring":                   false
            }

        Description:
            ID                          - Livestream ID
            Title                       - Livestream's Title
            Livestream                  - API Livestream URL
            Coding Category             - Coding Language/Category
            Difficulty                  - The language's difficulty!
            Start Time                  - The livestream's start time (Users TZ)
            Start Time (Original TZ)    - The livestream's start time (Streamers TZ)
            Is Featured                 - The user is featured
            Is Recurring                - The livestream is recurring
     */
    public class ScheduledBroadcast : APIResponse {
        int
            id;
        string
            title,
            livestream,
            coding_category,
            difficulty,
            start_time,
            start_time_original_timezone,
            original_timezone;
        bool
            is_featured,
            is_recurring;

        public int      ID                          { get { return this.id;                             } }
        public string   Title                       { get { return this.title;                          } }
        public string   LivestreamURL               { get { return this.title;                          } }
        public string   Difficulty                  { get { return this.difficulty;                     } }
        public string   StartTime                   { get { return this.start_time;                     } }
        public string   StartTime_OriginalTimeZone  { get { return this.start_time_original_timezone;   } }
        public bool     Featured                    { get { return this.is_featured;                    } }
        public bool     Recurring                   { get { return this.is_recurring;                   } }
    }

    // TODO: Get next and previous!
    public class ScheduledBroadcastList : APIResponseList<ScheduledBroadcast> { }
}
