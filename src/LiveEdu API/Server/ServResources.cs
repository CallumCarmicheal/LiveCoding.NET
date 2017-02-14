using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveEdu.Server {
    public class ServResources {
        public ServResources(string apiLocation) {
            LOCATION_APP = apiLocation;
        }

        private string LOCATION_APP               = ""; // http://callumcarmicheal.com/LCAPI/

        public string LOCATION_APP_API           { get { return LOCATION_APP + "apiApp.php"; } }
        public string LOCATION_APP_GUID_CHECK    { get { return LOCATION_APP + "apiCheckGUID.php"; } }
        public string LOCATION_APP_TOKEN_REFRESH { get { return LOCATION_APP + "apiRefreshToken.php"; } }
        public string LOCATION_APP_TOKEN_GET     { get { return LOCATION_APP + "apiGetBearer.php"; } }
    }
}
