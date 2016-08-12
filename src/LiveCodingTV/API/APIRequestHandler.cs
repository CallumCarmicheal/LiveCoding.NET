using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiveCodingTV.API {
    class APIRequestHandler {
        private static string FormatJson(string json) {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        public string getAPIJson(string apiReqURL, oAuthAuth oaCreds, bool formatted = false) {
            HttpWebRequest timeLineRequest =
                (HttpWebRequest)WebRequest.Create(apiReqURL);

            var apiReqHeaders = "{0} {1}";
            timeLineRequest.Headers.Add("Authorization",
                string.Format(apiReqHeaders, oaCreds.token_type, oaCreds.access_token));

            timeLineRequest.Method = "Get";

            WebResponse response = timeLineRequest.GetResponse();
            var responseJSON = string.Empty;
            using (response)
            using (var reader = new StreamReader(response.GetResponseStream()))
                responseJSON = reader.ReadToEnd();
            
            if (formatted)
                 return FormatJson(responseJSON);
            else return responseJSON;
        }
    }
}
