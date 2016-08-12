using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.API {

    // List of the wanted APICall
    public enum APICall {
        // Application
        Error,
        
        // API v2: CodingCategories
        CodingCategory,
        CodingCategories,

        // API v2: Language
        Language,
        Languages,

        // API v2: Livestreams
        Livestreams,
        LivestreamsOnAir,
        LivestreamUser,

        // API v2: ScheduledBroadcast
        ScheduledBroadcast,
        ScheduledBroadcasts,

        // API v2: User
        User,
        UserFollowers,
        UserFollows,
        UserCurrent, 
        UserXMPPDetails, 
        UserLivestreams,
        UserLivestreamsOnAir,
        UserVideos,
        UserVideosLatest,

        // API v2: Users
        UserPublicInfo,

        // API v2: Videos
        Video,
        Videos
    }

    public class Engine {
        private Server.Request req;


        public Engine(Server.Request request) {
            this.req = request;
        }


        public void CreateAPICall(APICall request) {

        }
    }
}
