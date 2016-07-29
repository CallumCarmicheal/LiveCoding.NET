using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public string GetSessionKey(string Code) {
            client.Headers.Clear();

            string Type = "authorization_code";
            string Return = engine.ReturnURL;
            string Url = "https://www.livecoding.tv/o/token/";

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("code", "%23+code%3D%22" + Code + "%22&");
            nvc.Add("grant_type", Type);
            nvc.Add("redirect_uri", Return);
            nvc.Add("Authentication", "Basic");

            client.Headers.Add("state",         engine._clientID);
            client.Headers.Add("secret_key",    engine._serverSecret);
            client.Headers.Add("Authentication", "basic");

            byte[] bResponse = client.UploadValues(Url, "POST", nvc);
            string sResponse = Encoding.UTF8.GetString(bResponse);

            Console.WriteLine("Reponse ->  {0}", sResponse);
            return sResponse;
        }

        public void SetupSession() {
            client.Headers.Clear();
            Console.WriteLine("API::Engine::WebEngine -> Cleared Headers");

            client.Headers.Add("Authorization: Bearer " + engine.getAPISessionKey());
            Console.WriteLine("API::Engine::WebEngine -> Set Authorization to Bearer");
            Console.WriteLine("API::Engine::WebEngine -> Set Secret [7] = " + engine.getAPISessionKey());
        }

        public string getReturnJSON(string link) {
            SetupSession();

            string compiledLocation = "https://www.livecoding.tv/" + link;

            Console.WriteLine("API::Engine::WebEngine -> Compiled Web Location = " + compiledLocation);
            return client.DownloadString(compiledLocation);
        }
    }
}
