using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.Models.Wrappers.Coding {


    /**

        JSON Schema:
            ICodingCategory : IAuthResponse {
                url (string),
                name (string),
                sort (integer)
            }

        JSON Result:
            {
              "url": "",
              "name": "",
              "sort": 0
            }

        Description:
            URL     - Url link to the coding category 
            Name    - Name of the coding category
            Sort    - ????
     */
    class ICodingCategory {
        public string
            url,
            name;
        public int
            sort;
    }
}
