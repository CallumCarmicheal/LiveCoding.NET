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
            CodingCategoriesSerializer {
                url     (string),
                name    (string),
                sort    (integer)
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
            Sort    - ID?
     */
    public class CodingCategory : APIClass {
        public string
            url,
            name;
        public int
            sort;
    }

    /**
        JSON Schema:
            CodingCategoriesSerializer {
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

    // TODO: Get next and previous!
    public class CodingCategoryList : APIResponseList<CodingCategory> {
        public CodingCategoryList GetNext(Engine eng) {
            return eng.CodingCategories.getList(next);
        }

        public CodingCategoryList GetPrevious(Engine eng) {
            return eng.CodingCategories.getList(previous);
        }
    }
}
