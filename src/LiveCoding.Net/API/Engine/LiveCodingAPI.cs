using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCoding.Net.API.Engine {
    class LiveCodingAPI {
        private string _apiKey;
        private WebEngine _webEngine;
        private APIEngine _apiEngine;

        public LiveCodingAPI(string APIKey) {
            this._apiKey = APIKey;

            _webEngine = new WebEngine(this);
            _apiEngine = new APIEngine(this);
        }

        public WebEngine getWebEngine() {
            return this._webEngine;
        }

        public APIEngine getAPIEngine() {
            return this._apiEngine;
        }

        public string getAPIKey() {
            return _apiKey;
        }
    }
}
