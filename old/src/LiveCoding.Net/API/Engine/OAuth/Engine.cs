using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LiveCoding.Net.API.Resources;
using RestSharp;


namespace LiveCoding.Net.API.Engine.OAuth {
    class Engine {
        public static string oAuthWebsite = "https://www.livecoding.tv/";
        public static string oAuthLocation = "o/token/";

        /*
            Getting token: https://www.livecoding.tv/o/token/
            Header Authentication Basic with the client_id and secret_key
            Code = ID OR SECRET?


            POST: 'code=%23+code%3D%221GWIr5oehziey5vQPZB4rBtYWGnxdY%22&grant_type=authorization_code&redirect_uri=http%3A%2F%2Flocalhost'
            Response: 
            {"access_token": "5mvm0v4JUgsVJtQUeEfgwnCz7Qxk23", "token_type": "Bearer", "expires_in": 36000, "refresh_token": "QyOhbpBe0odu3h4IzPP5IIHtOvQJ0d", "scope": "read"}
            */
        public static string GetTokenFromID(LiveCodingAPI api, string clientID, string clientSecret) {
            /*string request = "https://www.livecoding.tv/o/authorize?client_id={0}&response_type=code&state={1}";
            request = string.Format(clientID, "random_state_string");
            string resp = api.getWebEngine().getClient().DownloadString(request);
            ConAPI.WriteLine(resp, true, "RESP");*/
            //HttpWebResponse response = null;

            // Lets try it shall we?
            /*WebClient wc = api.getWebEngine().getClient();

            byte[] webResponse;

            webResponse = wc.UploadValues("https://www.livecoding.tv/o/token/", new NameValueCollection() {
                { "code",           clientID },
                { "grant_type",     "authorization_code" },
                { "redirect_uri",   "http://localhost:8080/Callum%20Carmicheal/LiveCoding/redirect.php" }
            });

            string result = System.Text.Encoding.UTF8.GetString(webResponse);*/
            return "";
        }

        /*public static IAuthResp GetToken(string token) {
            /*var client = new RestClient(oAuthWebsite);
            var request = new RestRequest(oAuthLocation, Method.POST);
            request.AddParameter("code", token);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("redirect_uri", "http://localhost");

            IRestResponse<IAuthResp> response = client.Execute<IAuthResp>(request);
            ConAPI.WriteLine(response.Content, true);
            return response.Data; // Dont know what it will return ATM
        }*/
    }
}
