using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static LiveCodingTV.API.Wrappers.Serializer;

namespace LiveCodingTV.API.Wrappers.Models {
    /**
        JSON Schema:
            ILanguageList {
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
    public class ILanguage {
        public string
            name,
            url;
    }

    /**
        JSON Schema:
            ILanguageList {
                count       (string),
                next        (string),
                previous    (string),
                results     (array : ILanguage)
            }

        JSON Results:
            {
              "count": 9,
              "next": null,
              "previous": null,
              "results": []
            }

        Description:
            Count       - This is the total count of coding requested feature
            Next        - The link to query the next set of requested feature
            Previous    - The link to query the previous set of the requested feature
            Results     - A array of the requested feature used on LiveCoding.tv
     */
    public class ILanguageList : IAPIResponseList {
        public ILanguage[] Languages {
            get {
                string json = results.ToString();
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var objs = JsonConvert.DeserializeObject<ILanguage[]>(json, set);
                return objs;
            }
        }
    }
}
