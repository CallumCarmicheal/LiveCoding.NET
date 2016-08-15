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

        public v2_CodingCategories      CodingCategories;
        public v2_Languages             Languages;
        public v2_Livestreams           Livestreams;
        public v2_ScheduledBroadcast    ScheduledBroadcasts;
        public v2_User                  User;
        public v2_Videos                Videos;

        public Engine(oAuthAuth token) {
            this.oaCreds = token;

            this.CodingCategories    = new v2_CodingCategories(aReq, oaCreds, ser);
            this.Languages           = new v2_Languages(aReq, oaCreds, ser);
            this.Livestreams         = new v2_Livestreams(aReq, oaCreds, ser);
            this.ScheduledBroadcasts = new v2_ScheduledBroadcast(aReq, oaCreds, ser);
            this.User                = new v2_User(aReq, oaCreds, ser);
            this.Videos              = new v2_Videos(aReq, oaCreds, ser);
        }

        public class v2_CodingCategories {
            APIRequestHandler aReq; oAuthAuth         oaCreds;
            Serializer        ser;

            public v2_CodingCategories(APIRequestHandler aReq, oAuthAuth oaCreds, 
                Serializer ser) {
                this.aReq = aReq; this.oaCreds = oaCreds;
                this.ser  = ser;
            }

            public CodingCategoryList getList() {
                var jsonString = aReq.getAPIJson(APIResources.CodingCategories, oaCreds, true);
                CodingCategoryList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
            public CodingCategoryList getList(string url) {
                var jsonString = aReq.getAPIJson(url, oaCreds, true);
                CodingCategoryList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
            
            public CodingCategory get(string Category) {
                var jsonString = aReq.getAPIJson(APIResources.getCodingCategory(Category), oaCreds, true);
                CodingCategory obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
        }

        public class v2_Languages {
            APIRequestHandler aReq; oAuthAuth oaCreds;
            Serializer ser;

            public v2_Languages(APIRequestHandler aReq, oAuthAuth oaCreds,
                Serializer ser) {
                this.aReq = aReq; this.oaCreds = oaCreds;
                this.ser = ser;
            }

            public LanguageList getList() {
                var jsonString = aReq.getAPIJson(APIResources.Languages, oaCreds, true);
                LanguageList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
            public LanguageList getList(string url) {
                var jsonString = aReq.getAPIJson(url, oaCreds, true);
                LanguageList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }


            public Language get(string Language) {
                var jsonString = aReq.getAPIJson(APIResources.getLangauge(Language), oaCreds, true);
                Language obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
        }

        public class v2_Livestreams {
            APIRequestHandler aReq; oAuthAuth oaCreds;
            Serializer ser;

            public v2_Livestreams(APIRequestHandler aReq, oAuthAuth oaCreds,
                Serializer ser) {
                this.aReq = aReq; this.oaCreds = oaCreds;
                this.ser = ser;
            }

            public LivestreamList getList() {
                var jsonString = aReq.getAPIJson(APIResources.Livestreams, oaCreds, true);
                LivestreamList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
            public LivestreamList getList(string url) {
                var jsonString = aReq.getAPIJson(url, oaCreds, true);
                LivestreamList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
            public LivestreamList getList_OnAIR() {
                var jsonString = aReq.getAPIJson(APIResources.LivestreamsOA, oaCreds, true);
                LivestreamList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
            public Livestream     get(string livestream) {
                var jsonString = aReq.getAPIJson(APIResources.getLivestream(livestream), oaCreds, true);
                Livestream obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
        }

        public class v2_ScheduledBroadcast {
            APIRequestHandler aReq; oAuthAuth oaCreds;
            Serializer ser;

            public v2_ScheduledBroadcast(APIRequestHandler aReq, oAuthAuth oaCreds,
                Serializer ser) {
                this.aReq = aReq; this.oaCreds = oaCreds;
                this.ser = ser;
            }

            public ScheduledBroadcastList getList() {
                var jsonString = aReq.getAPIJson(APIResources.ScheduleBroadcast, oaCreds, true);
                ScheduledBroadcastList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
            public ScheduledBroadcastList getList(string url) {
                var jsonString = aReq.getAPIJson(url, oaCreds, true);
                ScheduledBroadcastList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
            public ScheduledBroadcastList get(int ID) {
                var jsonString = aReq.getAPIJson(APIResources.getScheduledBroadcast("" + ID), oaCreds, true);
                ScheduledBroadcastList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
        }

        public class v2_User {
            APIRequestHandler aReq; oAuthAuth oaCreds;
            Serializer ser;

            public v2_User(APIRequestHandler aReq, oAuthAuth oaCreds,
                Serializer ser) {
                this.aReq = aReq; this.oaCreds = oaCreds;
                this.ser = ser;
            }

            public User getCurrent() {
                var jsonString = aReq.getAPIJson(APIResources.User, oaCreds, true);
                User obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
            public User getUser(string user) {
                var jsonString = aReq.getAPIJson(APIResources.getUser(user), oaCreds, true);
                User obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }

            public UserList Followers() {
                var jsonString = aReq.getAPIJson(APIResources.UserFollowers, oaCreds, true);
                UserList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }

            public UserList Follows() {
                var jsonString = aReq.getAPIJson(APIResources.UserFollows, oaCreds, true);
                UserList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }

            // TODO: Livestreams, Livestreams ONAIR, Viewing Key, 
            //       Chat account
            // WHY?: Livestreams:   I dont have any sample data so i cant tell if its a LivestreamLIST
            //                      or just a LIVESTREAM object.
            //       Chat account:  I dont think this will be accessable (yet atleast). 
            //       Viewing Key:   Same as current user
        }
        
        public class v2_Videos {
            APIRequestHandler aReq; oAuthAuth oaCreds;
            Serializer ser;

            public v2_Videos(APIRequestHandler aReq, oAuthAuth oaCreds,
                Serializer ser) {
                this.aReq = aReq; this.oaCreds = oaCreds;
                this.ser = ser;
            }

            public VideoList getList() {
                var jsonString = aReq.getAPIJson(APIResources.Videos, oaCreds, true);
                VideoList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }

            public VideoList getList(string url) {
                var jsonString = aReq.getAPIJson(url, oaCreds, true);
                VideoList obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }

            public Video get(string url) {
                var jsonString = aReq.getAPIJson(url, oaCreds, true);
                Video obj;
                var state = ser.toType(jsonString, out obj);
                if (state.Error) return null;
                else return obj;
            }
        }
        
    }
}
