using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.Models.Wrappers.Languages {
    /**
        JSON Schema:
            I : APIResponse {
                count (string),
                next (string),
                previous (string),
                results (array)
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
    class ILanguageList {
        public int
             count;
        public string
            next;
        public string
            previous;
        public ILanguage[]
            results;
    }
}
