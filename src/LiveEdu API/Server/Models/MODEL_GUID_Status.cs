using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveEdu.Server.Models {
    public enum GUID_Status {
        STATE_ID_NOTFOUND = 1,   // Not made or is invalid!
        STATE_ID_NOTYETVALID = 2,   // Created but waiting for response
        STATE_ID_VALID = 3    // VALID
    }

    public class MODEL_GUID_Status {
        public GUID_Status state;

        public bool Error;
        public string Error_Message;
    }
}
