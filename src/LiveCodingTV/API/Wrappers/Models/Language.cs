using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static LiveCodingTV.API.Wrappers.Serializer;

namespace LiveCodingTV.API.Wrappers.Models {

    public enum LanguageDifficulty {
        Beginner,
        Intermediate,
        Expert,
        Unknown
    }

    public class LanguageHelper {
        public static LanguageDifficulty toLD(string dif) {
            // There maybe more so im keeping it like this!
            string[]
                t_Beginner = {
                    "beginner"
                },

                t_Intermediate = {
                    "intermediate"
                },

                t_Expert = {
                    "expert"
                };


            dif = dif.ToLower();
            if (t_Beginner.Contains(dif))
                 return LanguageDifficulty.Beginner;
            else if (t_Intermediate.Contains(dif))
                 return LanguageDifficulty.Intermediate;
            else if (t_Intermediate.Contains(dif))
                 return LanguageDifficulty.Expert;
            else return LanguageDifficulty.Unknown;
        }
    }

    /**
        JSON Schema:
            SiteLanguagesSerializer {
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
    public class Language : APIResponse {
        public string
            name,
            url;
    }

    /**
        JSON Schema:
            SiteLanguagesSerializer {
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
    // TODO: Get next and previous!
    public class LanguageList : APIResponseList<Language> { }
}
