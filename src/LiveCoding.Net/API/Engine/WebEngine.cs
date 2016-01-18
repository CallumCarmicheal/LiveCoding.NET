using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LiveCoding.Net.API.Engine {
    class WebEngine {
        WebClient client;
        LiveCodingAPI engine;

        public WebEngine(LiveCodingAPI lca) {
            client = new WebClient();
            this. engine = lca;
        }

        public WebClient getClient() {
            return client;
        }

        public LiveCodingAPI getEngine() {
            return engine;
        }

        public string getReturnJSON(string link) {
            return client.DownloadString("https://www.livecoding.tv:443/" + link);
        }

    }
}
