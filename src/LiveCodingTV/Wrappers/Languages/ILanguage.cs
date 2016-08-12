using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.Models.Wrappers.Languages {




    /**
        JSON Schema:
            ILanguageList : APIResponse {
                name    (string),
                url     (string)
            }

        JSON Results:
            {
              "name": "",
              "url": ""
            }

        Description:
            Name    - Language Name
            URL     - Language URL
     */
    class ILanguage {
        public string
            name,
            url;
    }
}
