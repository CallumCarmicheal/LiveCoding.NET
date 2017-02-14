using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static LiveEdu.API.Wrappers.Serializer;

namespace LiveEdu.API.Wrappers {
    public enum HTTPResponse {
        Success         = 200,
        Unauthorised    = 403,
        Error           = 500,

        NoResponse      = 0 // Request recieved no response!
    }

    public enum ResponseState {
        Success,
        Error
    }

    public abstract class APIClass {
        Engine eng; // Why am i null ?
        public void API_SetEngine(Engine e) {
            eng = e;
        }

        public Engine API_GetEngine() {
            return eng;
        }
    }

    public abstract class APIResponse : APIClass {
        protected string               detail;
        public string ErrorDetails     { get { return detail; } }

        public bool                    Valid {
            get { return (detail == null); }
        }

    }

    public class APIResponseList<T> : APIClass {
        protected Serializer sz = new Serializer();

        // API RESPONSES
        protected int               count;
        protected string            next,  previous;
        protected JToken            results; // This should be serialized into the required list of objects

        private JsonSerializerSettings getSettings() {
            return new JsonSerializerSettings() { ContractResolver = new JSONnetPrivateManager(), NullValueHandling = NullValueHandling.Ignore };
        }

        public List<T> ToList() {
            string json = results.ToString();
            var objs    = JsonConvert.DeserializeObject<List<T>>(json, this.getSettings());
            return objs;
        }

        public T[] ToArray() {
            string json = results.ToString();
            var objs = JsonConvert.DeserializeObject<List<T>>(json, this.getSettings());
            return objs.ToArray<T>();
        }

        public int Count            { get { return this.count; } }
        public string URLNext       { get { return this.next; } }
        public string URLPrevious   { get { return this.previous; } }

        public APIResponseList<T> GetNext() {
            var url = this.URLNext;
            
            if (string.IsNullOrEmpty(url)) 
                return null;
            
            var eng = this.API_GetEngine();
            var req = eng.getRequestHandler();
            var oa  = eng.getOAuth();
            var ser = eng.getSerializer();

            var jsonString = req.getAPIJson(url, oa, true);

            APIResponseList<T> obj;
            var state = ser.toType(jsonString, out obj);
            if (state.Error) return null;
            else { obj.API_SetEngine(eng); return obj; }
        }
    }
}
