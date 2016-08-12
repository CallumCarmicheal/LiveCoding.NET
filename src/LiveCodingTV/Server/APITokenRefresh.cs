using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.Server {

    class APITokenRefresh {
        public enum State {
            Remake,  // Just remake the Request and start over (For reasons...)
            Success, // Everything went well.
            Retry    // Try again in a few minutes!
        }

        public State status;
        public string message;
    }
}
