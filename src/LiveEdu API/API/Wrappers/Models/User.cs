using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveEdu.API.Wrappers.Models {
    /**
    JSON Schema:
        UserSerializer || UserPrivateSerializer {
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
            timezone                            (string)                [Currently signed in user]
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
            Registration Date                   - Date the user registered on LiveEdu.tv
            Timezone                            - Timezone the user is in! (Only appears on /user/)
    */

    public class User : APIResponse {
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


    // TODO: Get Next and Previous!
    public class UserList       : APIResponseList<User> { }


    /**
    JSON Schema:
        XMPPAcountSerializer {
            user        (string),
            jid         (string),
            password    (string),
            color       (string),
            is_staff    (boolean)
        }

    JSON Results:
        {
            "user":       "",
            "jid":        "",
            "password":   "",
            "color":      "",
            "is_staff":   false
        }

    Description:
        User        - Username
        JID         - ????
        Password    - The XMPP Login Password
        Color       - The users chat color
        Is Staff    - If the user is global admin (staff)
    */
    public class UserXMPPAccount : APIResponse {
        string
            user,
            jid,
            password,
            color;
        bool
            is_staff;

        public string   User        { get { return this.user;       } }
        public string   JID         { get { return this.jid;        } }
        public string   Password    { get { return this.password;   } }
        public string   Color       { get { return this.color;      } }
        public bool     Staff       { get { return this.is_staff;   } }
    }

    /**
     JSON Schema:
         LiveStreamSerializer {
             url                (string),
             user               (string),
             user__slug         (string),
             title              (string),
             description        (string),
             coding_category    (string),
             difficulty         (string),
             language           (string),
             tags               (string),
             is_live            (boolean),
             viewers_live       (integer),
             viewing_urls       (string)
         }

     JSON Results:
         {
           "url":               "",
           "user":              "",
           "user__slug":        "",
           "title":             "",
           "description":       "",
           "coding_category":   "",
           "difficulty":        "",
           "language":          "",
           "tags":              "",
           "is_live":           false,
           "viewers_live":      0,
           "viewing_urls":      "",
           "streaming_key ":    "",
           "streaming_url ":    ""
         }

     Description:
         URL             - A link to the users livestream(s)
         User            - A link to the users profile
         User Slug       - The User's Username/Alias
         Title           - The stream title
         Description     - The user's description or stream description
         Coding Category - The category the livestream is under
         Language        - The language the livestream is in
         Tags            - The livestream tags
         Is Live         - If the user is currently livestreaming
         Viewers Live    - The amount of viewers currently on the stream
         Viewing URLS    - Urls to view/buffer the livestream from ????
         Streaming Key   - Key to stream to the user's channel
         Streaming URL   - Url to the user's livestream
    */
    public class UserLivestream : APIResponse {
        string
            url,
            user,
            user__slug,
            title,
            description,
            coding_category,
            difficulty,
            langauge,
            tags,
            thumbnail_url,
            streaming_key,
            streaming_url;
        string[]
            viewing_urls;
        bool
            is_live;
        int
            viewers_live;

        public string   URL                 { get { return this.url;                                                } }
        public string   Username            { get { return this.user;                                               } }
        public string   Slug                { get { return this.user__slug;                                         } }
        public string   Title               { get { return this.title;                                              } }
        public string   Description         { get { return this.description;                                        } }
        public string   CodingCategory      { get { return (this.coding_category == null ? "": coding_category);    } }
        public string   Difficulty          { get { return this.difficulty;                                         } }
        public string   Langauge            { get { return this.langauge;                                           } }
        public string   Tags                { get { return this.tags;                                               } }
        public string[] ViewingUrls         { get { return this.viewing_urls;                                       } }
        public string   ThumbnailURL        { get { return this.thumbnail_url;                                      } }
        public string   StreamingKey        { get { return this.streaming_key;                                      } }
        public string   StreamingURL        { get { return this.streaming_url;                                      } }
        public bool     IsLive              { get { return this.is_live;                                            } }
        public int      ViewerCount         { get { return this.viewers_live;                                       } }
    }

}