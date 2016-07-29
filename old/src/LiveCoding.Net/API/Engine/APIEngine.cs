using LiveCoding.Net.API.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LiveCoding.Net.API.Wrappers.Livestreams;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

namespace LiveCoding.Net.API.Engine {
    class APIEngine {
        LiveCodingAPI engine;

        public APIEngine(LiveCodingAPI engine) {
            this.engine = engine;
        }

        public string genGURL(string url) { return url; }
        public string genGURL(string url, string param) {
            return url.Replace("{0}", param);
        }


        public bool jsonSchemaIsValid(string schemaJSON, string json) {
            JsonSchema schema = JsonSchema.Parse(schemaJSON);
            JObject jsonObject = JObject.Parse(json);
            return (jsonObject.IsValid(schema));
        }

        public ILiveStreamInfo getLiveStreamUser(string un) {
            ILiveStreamInfo lsi = null;
            string requrl = genGURL(APIResources.LiveSteamsUser, un);
            string jsonReturn = engine.getWebEngine().getReturnJSON(requrl);

            if (jsonSchemaIsValid(Schema.ILiveStreamInfo, jsonReturn)) {
                try {
                    lsi = JsonConvert.DeserializeObject<ILiveStreamInfo>(jsonReturn);
                } catch {
                    System.Diagnostics.Debugger.Log(0, "Deserializing", "Could not deserialize a valid json schema for ILiveStreamInfo.");
                    lsi = null;
                }
            } return lsi;
        }
    }
}
