using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static LiveCoding.TV.NET_API_Wrapper.LiveCodingTV.API.Server.APISetupResponse;

namespace LiveCoding.TV.NET_API_Wrapper.LiveCodingTV.API.Server {
    class APISetupResponse {
        public class MODEL_Request_Response {
            public string GUID;
            public string URL;

            public bool Error;
            public string Error_Message;
        }

        public enum GUID_Status {
            STATE_ID_NOTFOUND = 1,      // Not made or is invalid!
            STATE_ID_NOTYETVALID = 2,   // Created but waiting for response
            STATE_ID_VALID = 3          // VALID
        }

        public class MODEL_GUID_Status {
            public GUID_Status state;

            public bool Error;
            public string Error_Message;
        }

        public class Request_API_STATE {
            public Request_API_STATE(string guid) {
                _GUID = guid;
            }

            private string _GUID;
            public string GUID { get { return _GUID; } }

            private bool _Expired = false;
            private bool _Valid = false;
            private bool _Cancel = false;


            public bool isValid() { return _Valid; }
            public bool isExpired() { return _Expired; }
            public bool isCancelled() { return _Cancel; }

            public void Cancel() { _Cancel = true; }
        }

        public enum ResponseState {
            ERROR,
            TIMEOUT,
            RETURN, // Used when the application does not wait to check!
            EXPIRED,
            SUCCESS
        }


        private ResponseState state;
        private string Message;

        public ResponseState getState() { return this.state;   }
        public string getMessage()      { return this.Message; }
        
        public APISetupResponse(ResponseState st, string Message) {
            this.Message = Message;
            this.state = st;
        }
    }
    
    class Request {
        public delegate void API_STATE(ref APISetupResponse evt);

        private string _GUID;
        private string _URL;

        public string GUID { get { return _GUID; } }
        public string URL  { get { return _URL; } }



        WebClient wc = new WebClient();

        public GUID_Status getGUIDState() {
            // Checks if the guid is null/whitespaced
            if(string.IsNullOrWhiteSpace(_GUID)) return GUID_Status.STATE_ID_NOTFOUND;

            string url = Resources.LOCATION_APP_GUID_CHECK + "?g=" + _GUID;
            string js = wc.DownloadString(url);
            var obj = JsonConvert.DeserializeObject<MODEL_GUID_Status>(js);

            return obj.state;
        }

        public MODEL_Request_Response getNewGUID() {
            string js = wc.DownloadString(Resources.LOCATION_APP_API);
            var obj = JsonConvert.DeserializeObject<MODEL_Request_Response>(js);
            _GUID = obj.GUID;
            _URL = obj.URL;
            return obj;
        }
        
        // TODO: CREATE SOME TYPE OF CALLBACK SYSTEM
        public void SetupAPIRequest_ASYNC() { }

        // Max Wait 30 secs for response
        // WaitTimeOutMS will freeze the current thread so use wisely!
        public APISetupResponse SetupAPIRequest(bool WaitUntilValid, int WaitTimeOutMS = 300, int MaxWaitTime = 30000) {
            DateTime dt     = DateTime.Now;
            DateTime dtExp  = DateTime.Now.AddMilliseconds(MaxWaitTime);

        GenerateNewGUID:
            if (getGUIDState() == GUID_Status.STATE_ID_NOTFOUND)
                getNewGUID();
            
            // Open web browser for the user to authorise the code etc.
            System.Diagnostics.Process.Start(_URL);

            bool exit = false;
            while (!exit) {
                if(dt > dtExp) {
                    // Our request has expired for the user
                    // We want to cancel and exit the thread
                    return new APISetupResponse(APISetupResponse.ResponseState.TIMEOUT, "Timeout reached!");
                }


                // Contact server to check if the 
                var resp = getGUIDState();

                if(resp == GUID_Status.STATE_ID_VALID) 
                    return new APISetupResponse(APISetupResponse.ResponseState.SUCCESS, "Token was authenticated!");
                else if (resp == GUID_Status.STATE_ID_NOTFOUND) {
                    exit = false;
                    goto GenerateNewGUID;
                }

                if(!WaitUntilValid)
                    return new APISetupResponse(APISetupResponse.ResponseState.RETURN, "Returned, Use your own code to check if the guid is correct!!!");

                // Keep waiting
                System.Threading.Thread.Sleep(WaitTimeOutMS);
            } return new APISetupResponse(APISetupResponse.ResponseState.ERROR, "No response from catches!");
        }
    }
}
