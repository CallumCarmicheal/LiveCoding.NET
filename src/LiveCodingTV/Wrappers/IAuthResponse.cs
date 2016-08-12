using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.Models.Wrappers {
    enum HTTPResponse {
        Success         = 200,
        Unauthorised    = 403,
        Error           = 500
    }

    enum ResponseState {
        Success,
        Error
    }



    class IAuthResponseList {
        public HTTPResponse     HttpResponse;

        // Simplified version of HttpResponse
        public ResponseState Status {
            get {
                return HttpResponse == HTTPResponse.Success ? ResponseState.Success : ResponseState.Error;
            }
        }

        // API RESPONSES
        public int              Count;
        public string           Next,  Previous;
        public virtual object   GetResults() { return null; } // This should be overriden by the child class.
    }
}
