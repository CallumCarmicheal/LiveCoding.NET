using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static LiveCodingTV.API.Wrappers.Serializer;

namespace LiveCodingTV.API.Wrappers {
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

    public abstract class APIResponse {
        protected string               detail;
        public string ErrorDetails     { get { return detail; } }

        public bool                    Valid {
            get { return (detail == null); }
        }
    }

    public abstract class APIResponseList<T> {
        protected Serializer sz = new Serializer();

        // API RESPONSES
        protected int               count;
        protected string            next,  previous;
        protected JToken            results; // This should be serialized into the required list of objects

        public List<T> ToList() {
            string json = results.ToString();
            var set     = new JsonSerializerSettings() { ContractResolver = new JSONnetPrivateManager() };
            var objs    = JsonConvert.DeserializeObject<List<T>>(json, set);
            return objs;
        }

        public int Count            { get { return this.count; } }
        public string URLNext       { get { return this.next; } }
        public string URLPrevious   { get { return this.previous; } }

        
    }
}
