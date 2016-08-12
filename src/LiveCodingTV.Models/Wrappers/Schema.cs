using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.Models.Wrappers {
    class Schema {
        public static string
            ILiveStreamInfo = @"
                {
	                'url' : {'type': 'string'},
	                'user' : {'type': 'string'},
	                'user__slug' : {'type': 'string'},
	                'title' : {'type': 'string'},
	                'description' : {'type': 'string'},
	                'coding_category' : {'type': 'string'},
	                'difficulty' : {'type': 'string'},
	                'language' : {'type': 'string'},
	                'tags' : {'type': 'string'},
	                'is_live' : {'type': 'boolean'},
	                'viewers_live' : {'type': 'integer'},
	                'viewing_urls' : {'type': 'string'}
                }
            ",

            IRequestArray = @"
                {
	                count : {'type': 'string'},
	                next : {'type': 'string'},
	                previous : {'type': 'string'},
	                results : {'type': 'array'}
                }
            ",
            
            JUSTANOTHERPLACEHOLDER = "";
    }
}
