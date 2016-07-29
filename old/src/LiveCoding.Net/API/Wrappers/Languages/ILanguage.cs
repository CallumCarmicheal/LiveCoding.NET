using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCoding.Net.API.Wrappers.Languages {




    /**
        JSON Schema:
            ILanguageList : SiteLanguagesSerializer {
                name (string),
                url (string)
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
