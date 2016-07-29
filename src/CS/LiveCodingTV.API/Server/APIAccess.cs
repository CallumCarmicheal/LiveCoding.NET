using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiveCoding.TV.NET_API_Wrapper.LiveCodingTV.API.Server {
    class APITokenRefresh {
        public enum State {
            Remake,  // Just remake the Request and start over (For reasons...)
            Success, // Everything went well.
            Retry    // Try again in a few minutes!
        }

        public State  status;
        public string message;
    }


    class APIAccess {
        class MODEL_Refresh_Response {
            public enum Status {
                ERROR, // RUN FoR THE HILLS!
                SUCCESS
            }

            public Status status;
        }

        class MODEL_Bearer_Response {
            public string code;

            public bool Error;
            public string Error_Message;
        }

        WebClient wc = new WebClient();
        public Request req;

        private string _bearer;
        public string Bearer { get { return _bearer; } }

        public APIAccess(Request request) {
            this.req = request;

            // Okay the token we originally setup 
            // has become invalid
            if(!isValid()) RefreshToken();

            // Get our code
            getBearerCode();
        }

        public bool isValid() {
            if (req.getGUIDState() != APISetupResponse.GUID_Status.STATE_ID_VALID)
                return false;

            // TODO: Do a test on a light weight api element
            // check response!

            return false;
        }

        public void getBearerCode() {
            string url = Resources.LOCATION_APP_TOKEN_GET;
            url += "?g=" + req.GUID;
            string json = wc.DownloadString(url);
            var obj = JsonConvert.DeserializeObject<MODEL_Bearer_Response>(json);
            _bearer = obj.code;
        }

        public APITokenRefresh RefreshToken() {
            // Check if a refresh is really required!
            if (isValid()) return new APITokenRefresh() { status = APITokenRefresh.State.Success, message = "Everything works fine, your just crazy!" };

            // Check if the guid as expired
            var state = req.getGUIDState();
            if(state == APISetupResponse.GUID_Status.STATE_ID_NOTFOUND) 
                return new APITokenRefresh() { status = APITokenRefresh.State.Remake, message = "The token and guid have expired," };

            // Attempt to refresh the token!

            string url = Resources.LOCATION_APP_TOKEN_REFRESH;
            url += "?g=" + req.GUID;
            string json = wc.DownloadString(url);
            var obj = JsonConvert.DeserializeObject<MODEL_Refresh_Response>(json);

            if (obj.status == MODEL_Refresh_Response.Status.SUCCESS)
                 return new APITokenRefresh() { status = APITokenRefresh.State.Success, message = "Everything went swell!" };
            else return new APITokenRefresh() { status = APITokenRefresh.State.Retry, message = "There was a problem refreshing your token!" };
        }

    }
}
