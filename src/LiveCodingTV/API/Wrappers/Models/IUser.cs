using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.API.Wrappers.Models {
    /**
    JSON Schema:
        IUser  {
            url                                 (string),
            username                            (string),
            slug                                (string),
            avatar                              (string),
            country                             (string),
            city                                (string),
            favorite_programming                (string),
            favorite_ide                        (string),
            favorite_coding_background_music    (string),
            favorite_code                       (string),
            years_programming                   (integer),
            want_learn                          (array : string),
            registration_date                   (string),
            registration_date                   (string)                [Currently signed in user]
        }

    JSON Results:
        {
          "url": "",
          "username": "",
          "slug": "",
          "avatar": "",
          "country": "",
          "city": "",
          "favorite_programming": "",
          "favorite_ide": "",
          "favorite_coding_background_music": "",
          "favorite_code": "",
          "years_programming": 0,
          "want_learn": [
            null
          ],
          "registration_date": "",
          "timezone": ""
        }

    Description:
            URL                                 - Url to the user
            Username                            - The user's username
            Slug                                - The user's unique username (original name) known as slug
            Avatar                              - A link to a image that the user uses as their avatar
            Country                             - What country the user is from
            City                                - What city the user is from
            Favorite Programming                - Favorite Programming Language
            Favorite IDE                        - Favorite IDE the user likes to use
            Favorite coding background music    - Favorite background music (a feature they may add o_0)
            Favorite Code                       - Favorite Line of code!
            Years Programming                   - How many years the user has been programming
            Want Learn                          - A array of strings (up to 3), 
            Registration Date                   - Date the user registered on LiveCodingTV
            Timezone                            - Timezone the user is in! (Only appears on /user/)
    */

    public class IUser : IAPIResponse {
        string          url,
                        username,
                        slug,
                        avatar,
                        country,
                        city,
                        favorite_programming,
                        favorite_ide,
                        favorite_code;
        int             years_programming;
        string[]        want_learn;
        string          registration_date,
                        timezone;
        

        public string   URL                 { get { return this.url;                    } }
        public string   Username            { get { return this.username;               } }
        public string   Slug                { get { return this.slug;                   } }
        public string   Avatar              { get { return this.avatar;                 } }
        public string   Country             { get { return this.country;                } }
        public string   City                { get { return this.city;                   } }
        public string   FavoriteLanguage    { get { return this.favorite_programming;   } }
        public string   FavoriteIDE         { get { return this.favorite_ide;           } }
        public string   FavoriteLineOfCode  { get { return this.favorite_code;          } }
        public int      YearsProgramming    { get { return this.years_programming;      } }
        public string[] WantToLearn         { get { return this.want_learn;             } }
        public string   DateRegistered      { get { return this.registration_date;      } }
        public string   Timezone            { get { return this.timezone;               } }
    }
}
