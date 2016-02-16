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
        public string _clientID; // TODO::MAKEPRIVATELATER->JUSTDOIT(bool *AMIGOINGTODOIT_OR_WILLINOT);
        public string _clientSecret;

        public LiveCodingAPI(string clientID, string clientSecret) {
            _clientID = clientID;
            _clientSecret = clientSecret;

            _webEngine = new WebEngine(this);
            _apiEngine = new APIEngine(this);


            // Get Access Key
            string str = OAuth.Engine.GetTokenFromID(this, _clientID, _clientSecret);
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
            return this._clientSecret;
        }
    }
}
