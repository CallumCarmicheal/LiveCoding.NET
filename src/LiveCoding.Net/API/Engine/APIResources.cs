using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCoding.Net.API.Engine {
    class APIResources {

        /* All API Links */
        public static string
                /* Coding */
                CodingCategoryList      = "api/codingcategories/",
                CodingCategoryInfo      = "api/codingcategories/{0}/",

            /* Languages */
                LanguageList            = "api/languages/",
                LanguageInfo            = "api/languages/{0}/",

            /* Livestreams */
                LiveSteamsList          = "api/livestreams/",
                LiveSteamsOnAir         = "api/livestreams/onair/",
                LiveSteamsUser          = "api/livestreams/{0}/",

            /* Scheduled Boradcasts */
                ScheduledBroadcastsList = "api/scheduledbroadcast/",
                ScheduledBroadcastsInfo = "api/scheduledbroadcast/{0}/",

            /* User Information (Requires auth) */
                UserList                = "/api/user/",
                UserFollowers           = "/api/user/follows/",
                UserFollowing           = "/api/user/viewing_key/",
                UserViewingKey          = "/api/user/chat/account/",
                UserLivestreams         = "/api/user/livestreams/",
                UserLivestreamsOnAir    = "/api/user/livestreams/onair/",
                UserVideos              = "/api/user/videos/",
                UserVideosLatest        = "/api/user/videos/latest/",

            /* User Information (Public) */
                UserGetPublicInfo       = "api/users/{0}",

            /* Videos */
                VideosList              = "api/videos/",
                VideosGet               = "api/videos/{0}/",    
             
            /* V1 */
                // WILL DO LATER
                // NOT IMPLEMENTED

            /* Place Holder */
                LISTEDBYCALLUMC        = "with <3";
    }
}
