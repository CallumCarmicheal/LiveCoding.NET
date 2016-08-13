using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCodingTV.API.Wrappers;
using LiveCodingTV.API.Wrappers.Models;

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
        APIRequestHandler aReq = new APIRequestHandler();
        oAuthAuth         oaCreds;
        Serializer        ser = new Serializer();

        public Engine(oAuthAuth token) {
            this.oaCreds = token;
        }


        public IUser getCurrentUser() {
            var jsonString = aReq.getAPIJson(APIResources.User, oaCreds, true);

            IUser obj;
            var state      = ser.toUser(jsonString, out obj);
            if (state.Error)
                 return null;
            else return obj;
        }

        public IUser getUser(string user) {
            var jsonString = aReq.getAPIJson($"{APIResources.Users}{user}/", oaCreds, true);

            IUser obj;
            var state = ser.toUser(jsonString, out obj);
            if (state.Error)
                 return null;
            else return obj;
        }

        public ILivestreamList getLivestreamsFromURL(string url) {
            var jsonString = aReq.getAPIJson(url, oaCreds, true);

            ILivestreamList obj;
            var state = ser.toLivestreams(jsonString, out obj);
            if (state.Error)
                 return null;
            else return obj;
        }

        public ILivestreamList getLivestreamsOnAIR() {
            var jsonString = aReq.getAPIJson(APIResources.LivestreamsOA, oaCreds, true);

            ILivestreamList obj;
            var state = ser.toLivestreams(jsonString, out obj);
            if (state.Error)
                 return null;
            else return obj;
        }
    }
}
