using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCoding.Net.API.Engine {
    class LiveCodingAPI {
        private WebEngine _webEngine;
        private APIEngine _apiEngine;

        private string _apiSessionKey = "";
        private string _apiStateKey = "";
        public string _clientID; // TODO::MAKEPRIVATELATER->JUSTDOIT(bool *AMIGOINGTODOIT_OR_WILLINOT);
        public string _clientSecret;

        public string _serverID;
        public string _serverSecret;

        public string ReturnURL = "callumcarmicheal.com/LiveCoding/Response.php";

        public LiveCodingAPI(string clientID, string clientSecret) {
            _clientID = clientID;
            //_clientSecret = clientSecret;

            _webEngine = new WebEngine(this);
            _apiEngine = new APIEngine(this);

            // Get Access Key
            //string str = OAuth.Engine.GetTokenFromID(this, _clientID, _clientSecret);
            string[] bypassKey = System.IO.File.ReadAllLines("bin/temp/session.txt");
            _apiStateKey    = bypassKey[0];
            _clientID       = bypassKey[0];
            _clientSecret   = bypassKey[1];

            string[] bypassServer = System.IO.File.ReadAllLines("bin/temp/server.txt");
            _serverID       = bypassServer[0];
            _serverSecret   = bypassServer[1];
            
            Console.WriteLine("API::INIT -> Set State({0}), Set CliSecret({1})", _apiStateKey, _apiSessionKey);

            _apiSessionKey = getWebEngine().GetSessionKey(_clientSecret);
            
        }


        /*public LiveCodingAPI(string APIKey, bool GetAsWebServer = false) {
            _apiSessionKey = APIKey;
            // Get API Access Key
            if (GetAsWebServer) {
                _apiToken = OAuth.Engine.GetToken(APIKey);
                Resources.ConAPI.WriteLine(
                    "Token Expires in: " + _apiToken.expires_in,
                    true,
                    "APIKey");
            }


            _webEngine = new WebEngine(this);
            _apiEngine = new APIEngine(this);
        }*/

        public WebEngine getWebEngine() {
            return this._webEngine;
        }

        public APIEngine getAPIEngine() {
            return this._apiEngine;
        }

        /*public OAuth.IAuthResp getAPISession() {
            return this._apiSessionKey;
        }*/

        public string getAPISessionKey() {
            return this._apiSessionKey;
        }

        public string getClientSecret() {
            return this._clientSecret;
        }
    }
}
