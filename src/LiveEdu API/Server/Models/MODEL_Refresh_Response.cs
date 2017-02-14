using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveEdu.Server.Models {
    public class MODEL_Refresh_Response {
        public enum Status {
            ERROR = 0, // RUN FoR THE HILLS!
            SUCCESS = 1
        }

        public Status status;
    }
}
