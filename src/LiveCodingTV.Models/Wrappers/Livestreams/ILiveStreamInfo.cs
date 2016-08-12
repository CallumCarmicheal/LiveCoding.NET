using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.Models.Wrappers.Livestreams {
    /**
        JSON Schema:
            ILiveStreamInfo : APIResponse  {
                url (string),
                user (string),
                user__slug (string),
                title (string),
                description (string),
                coding_category (string),
                difficulty (string),
                language (string),
                tags (string),
                is_live (boolean),
                viewers_live (integer),
                viewing_urls (string)
            }

        JSON Results:
            {
              "url": "",
              "user": "",
              "user__slug": "",
              "title": "",
              "description": "",
              "coding_category": "",
              "difficulty": "",
              "language": "",
              "tags": "",
              "is_live": false,
              "viewers_live": 0,
              "viewing_urls": ""
            }

        Description:
            This bit is gonna suck for me (17/01/2017)

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
     */
    class ILiveStreamInfo {
        public string
            url,
            user,
            user__slug,
            title,
            description,
            coding_category,
            difficulty,
            langauge,
            tags,
            viewing_urls,
            thumbnail_url;
        public bool
            is_live;
        public int
            viewers_live;
    }
}
