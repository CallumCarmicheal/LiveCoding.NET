using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCodingTV.Server.Models;

namespace LiveCodingTV.Server {
    public class APISetupResponse {
        private ResponseState state;
        private string Message;

        public ResponseState getState() { return this.state; }
        public string getMessage() { return this.Message; }

        public APISetupResponse(ResponseState st, string Message) {
            this.Message = Message;
            this.state = st;
        }
    }
}
