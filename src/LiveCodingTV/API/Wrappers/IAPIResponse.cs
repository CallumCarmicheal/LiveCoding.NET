using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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

    public abstract class IAPIResponse {
        protected string               detail;

        public string ErrorDetails  { get { return detail; } }

        public bool                 Valid {
            get { return (detail == null); }
        }
    }

    public abstract class IAPIResponseList {
        protected Serializer sz = new Serializer();

        // API RESPONSES
        protected int               count;
        protected string            next,  previous;
        protected JToken            results; // This should be serialized into the required list of objects


        public int Count            { get { return this.count; } }
        public string URLNext       { get { return this.next; } }
        public string URLPrevious   { get { return this.previous; } }
    }
}
