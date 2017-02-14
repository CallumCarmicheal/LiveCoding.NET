using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.API {
    public class APIResources {
        // OA = OnAir
        
        public static string
            APIBasePath         = "https://www.liveedu.tv:443/api/",
            
            CodingCategories    = APIBasePath   + "codingcategories/",
            Languages           = APIBasePath   + "languages/",
            Livestreams         = APIBasePath   + "livestreams/",
            LivestreamsOA       = Livestreams   + "onair/",
            ScheduleBroadcast   = APIBasePath   + "schedulebroadcast/",

            User                = APIBasePath   + "user/",
            UserFollowers       = User          + "followers/",
            UserFollows         = User          + "follows/",
            UserViewingKey      = User          + "viewing_key/",
            UserXMPP            = User          + "chat/account/",
            UserLivestreams     = User          + "livestreams/",
            UserLivestreamsOA   = User          + "livestreams/onair/",
            UserVideos          = User          + "videos/",
            UserVideosLatest    = User          + "videos/latest/",

            Users               = APIBasePath   + "users/",

            PublicUserInfo      = APIBasePath   + "users/",
            Videos              = APIBasePath   + "videos/";


        public static string getCodingCategory(string categoryName) {
            return CodingCategories + categoryName + "/";
        }

        public static string getLangauge(string language) {
            return Languages + language + "/";
        }

        public static string getLivestream(string user__Slug) {
            return Livestreams + user__Slug + "/";
        }

        public static string getScheduledBroadcast(string broadcastID) {
            return ScheduleBroadcast + broadcastID + "/";
        }

        public static string getUser(string userName) {
            return PublicUserInfo + userName + "/";
        }

        public static string getVideos(string videoID) {
            return Videos + videoID + "/";
        }
    }
}
