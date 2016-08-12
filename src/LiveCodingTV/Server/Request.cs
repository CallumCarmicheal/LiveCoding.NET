using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LiveCodingTV.Server.Models;
using Newtonsoft.Json;

using static LiveCodingTV.Server.APISetupResponse;

namespace LiveCodingTV.Server {
    public class Request {
        private ServResources servRes;
        public  ServResources getServerResources() { return this.servRes; }

        public  Request(ServResources serverResources) { this.servRes = serverResources; }

        public delegate void API_STATE(ref APISetupResponse evt);

        private string _GUID;
        private string _URL;

        public  string GUID { get { return _GUID; } }
        public  string URL  { get { return _URL; } }
        
        WebClient wc = new WebClient();

        public GUID_Status getGUIDState() {
            // Checks if the guid is null/whitespaced
            if(string.IsNullOrWhiteSpace(_GUID)) return GUID_Status.STATE_ID_NOTFOUND;

            string url = servRes.LOCATION_APP_GUID_CHECK + "?g=" + _GUID;
            string js = wc.DownloadString(url);
            var obj = JsonConvert.DeserializeObject<MODEL_GUID_Status>(js);

            return obj.state;
        }

        public MODEL_Request_Response getNewGUID() {
            string js = wc.DownloadString(servRes.LOCATION_APP_API);
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
                    return new APISetupResponse(ResponseState.TIMEOUT, "Timeout reached!");
                }

                // Contact server to check if the 
                var resp = getGUIDState();

                if(resp == GUID_Status.STATE_ID_VALID) 
                    return new APISetupResponse(ResponseState.SUCCESS, "Token was authenticated!");
                else if (resp == GUID_Status.STATE_ID_NOTFOUND) {
                    exit = false;
                    goto GenerateNewGUID;
                }

                if(!WaitUntilValid)
                    return new APISetupResponse(ResponseState.RETURN, "Returned, Use your own code to check if the guid is correct!!!");

                // Keep waiting
                System.Threading.Thread.Sleep(WaitTimeOutMS);
            } return new APISetupResponse(ResponseState.ERROR, "No response from catches!");
        }
    }
}
