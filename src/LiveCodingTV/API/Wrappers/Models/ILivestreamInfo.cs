﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static LiveCodingTV.API.Wrappers.Serializer;

namespace LiveCodingTV.API.Wrappers.Models {
    /**
        JSON Schema:
            ILivestream  {
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
    public class ILivestream {
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
            thumbnail_url;
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
        public bool     IsLive              { get { return this.is_live;                                            } }
        public int      ViewerCount         { get { return this.viewers_live;                                       } }
    }

    /**
        JSON Schema:
            ILivestream : IAuthResponseList {
                count       (string),
                next        (string),
                previous    (string),
                results     (array : ILivestream)
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
    public class ILivestreamList : IAPIResponseList {
        public ILivestream[] Livestreams {
            get {
                string json = results.ToString();
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var objs = JsonConvert.DeserializeObject<ILivestream[]>(json, set);
                return objs;
            }
        }

        public ILivestreamList GetNext(Engine eng) {
            return eng.getLivestreamsFromURL(next);
        }

        public ILivestreamList GetPrevious(Engine eng) {
            return eng.getLivestreamsFromURL(previous);
        }
    }
}
