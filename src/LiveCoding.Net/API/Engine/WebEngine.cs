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

        public void SetupSession() {
            client.Headers.Clear();
            Console.WriteLine("API::Engine::WebEngine -> Cleared Headers");

            client.Headers.Add("Authorization: Bearer " + engine.getAPISessionKey());
            Console.WriteLine("API::Engine::WebEngine -> Set Authorization to Bearer");
            Console.WriteLine("API::Engine::WebEngine -> Set Secret [7] = " + engine.getAPISessionKey().Substring(0, 6));
        }

        public string getReturnJSON(string link) {
            SetupSession();

            string compiledLocation = "https://www.livecoding.tv/" + link;

            Console.WriteLine("API::Engine::WebEngine -> Compiled Web Location = " + compiledLocation);
            return client.DownloadString(compiledLocation);
        }
    }
}
