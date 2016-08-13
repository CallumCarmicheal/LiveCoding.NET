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
            ICodingCategory {
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
    public class ICodingCategory {
        public string
            url,
            name;
        public int
            sort;
    }

    /**
        JSON Schema:
            ICodingCategoryList {
                count       (string),
                next        (string),
                previous    (string),
                results     (array : ICodingCategory)
            }

        JSON Results: {
              "count":      0,
              "next":       "",
              "previous":   "",
              "results":    []
            }

        Description:
            Count       - This is the total count of coding categories
            Next        - The link to query the next set of categories
            Previous    - The link to query the previous set of categories
            Results     - A array of the cateogies used on LiveCoding.tv
     */
    public class ICodingCategoryList : IAPIResponseList {
        public ICodingCategory[] Categories {
            get {
                string json = results.ToString();
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var objs = JsonConvert.DeserializeObject<ICodingCategory[]>(json, set);
                return objs;
            }
        }
    }
}
