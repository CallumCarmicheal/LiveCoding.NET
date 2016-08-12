using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.Models.Wrappers.Coding {
    /**
        JSON Schema:
            ICodingCategoryList : IAuthResponseList {
                count       (string),
                next        (string),
                previous    (string),
                results     (ICodingCategory[] : IAuthResponseList)
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
    class ICodingCategoryList {
        public int
            count;
        public string
            next;
        public string
            previous;
        public ICodingCategory[] 
            results;
    }
}
