using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveEdu.Server.Models {
    public enum ResponseState {
        ERROR,
        TIMEOUT,
        RETURN, // Used when the application does not wait to check!
        EXPIRED,
        SUCCESS
    }

    class MODEL_Request_API_STATE {
        public MODEL_Request_API_STATE(string guid) { _GUID = guid; }

        private string  _GUID;
        public string   GUID                { get { return _GUID; } }

        private bool    _Expired            = false;
        private bool    _Valid              = false;
        private bool    _Cancel             = false;
        
        public bool     isValid()           { return _Valid; }
        public bool     isExpired()         { return _Expired; }
        public bool     isCancelled()       { return _Cancel; }

        public void     Cancel()            { _Cancel = true; }
    }
}
