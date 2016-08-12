using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.API {
    public class oAuthAuth {
        public oAuthAuth() { }

        public oAuthAuth(string access_token, string token_type = "Bearer") {
            this.token_type   = token_type;
            this.access_token = access_token;
        }

        public string token_type { get; set; }
        public string access_token { get; set; }
    }
}
