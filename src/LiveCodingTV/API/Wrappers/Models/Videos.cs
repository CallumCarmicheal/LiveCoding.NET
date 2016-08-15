using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCodingTV.API.Wrappers.Models {
    /**
    JSON Schema:
        VideoSerializer {
            url             (string),
            slug            (string),
            user            (string),
            title           (string),
            description     (string),
            coding_category (string),
            difficulty      (string),
            language        (string),
            product_type    (string) = ['game' or 'app' or 'website' or 'codetalk' or 'other'],
            creation_time   (string),
            duration        (integer),
            region          (string) = ['us-stlouis' or 'eu-london'],
            viewers_overall (integer),
            viewing_urls    (string),
            thumbnail_url   (string)
        }

    JSON Results:
        {
          "url":                "",
          "slug":               "",
          "user":               "",
          "title":              "",
          "description":        "",
          "coding_category":    "",
          "difficulty":         "",
          "language":           "",
          "product_type":       "",
          "creation_time":      "",
          "duration":           0,
          "region":             "",
          "viewers_overall":    0,
          "viewing_urls":       "",
          "thumbnail_url":      ""
        }
    Description:
        URL                 - API Url
        Slug                - Username / SLUG
        User                - Username
        Title               - Video Title
        Description         - Video's description
        Coding Category     - Coding Category
        Difficulty          - Difficulty of the Product etc.
        Language            - Spoken/Written (Language) Language 
        Product Type        - Type of product, EG. Mobile APP, Website.
        Creation Time       - Time the video/livestream was created
        Duration            - Time in Seconds
        Region              - Video Region
        Viewers Overall     - How many viewers in total has seen the video
        Viewing URLS        - Direct URL to view and download the livestream
        Thumnail URL        - Video's thumnail
    */
    public class Video : APIResponse {
        string
            url,
            slug,
            user,
            title,
            description,
            coding_category,
            difficulty,
            language,
            product_type,
            creation_time,
            region,
            thumnail_url;
        int
            duration,
            viewers_overall;
        string[]
            viewing_url;

        public string               URL                     { get { return this.url;                             } }
        public string               Slug                    { get { return this.slug;                            } }
        public string               Username                { get { return this.user;                            } }
        public string               Title                   { get { return this.title;                           } }
        public string               Description             { get { return this.description;                     } }
        public string               CodingCategory          { get { return this.coding_category;                 } }
        public string               Language                { get { return this.language;                        } }
        public string               ProductType             { get { return this.product_type;                    } }
        public string               Region                  { get { return this.region;                          } }
        public string               Thumnail                { get { return this.thumnail_url;                    } }
        public int                  Duration                { get { return this.duration; } }
        public int                  TotalViewers            { get { return this.viewers_overall;                 } }
        public string[]             ViewingURL              { get { return this.viewing_url;                     } }
        public LanguageDifficulty   Difficulty    { get { return LanguageHelper.toLD(this.difficulty); } }
        public DateTime             CreationTime            { get { return DateTime.Parse(this.creation_time); } }
    }

    // TODO: Get Next, Previous!
    public class VideoList : APIResponseList<Video> { }
}
