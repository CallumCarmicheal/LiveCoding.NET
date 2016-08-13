using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LiveCodingTV.API.Wrappers.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace LiveCodingTV.API.Wrappers {
    public class Serializer_Status {
        public Exception Exception;
        public bool      Error = false;
    }

    public class Serializer {
        public class MyContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
                var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                .Select(p => base.CreateProperty(p, memberSerialization))
                            .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                       .Select(f => base.CreateProperty(f, memberSerialization)))
                            .ToList();
                props.ForEach(p => { p.Writable = true; p.Readable = true; });
                return props;
            }
        }

        public Serializer_Status toCodingCategory    (string json, out ICodingCategory @out)        {
            Serializer_Status status = new Serializer_Status();

            try {
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var resp = JsonConvert.DeserializeObject<ICodingCategory>(json, set);
                @out = resp; return status;
            } catch (Exception ex) {
                status.Error = true;
                status.Exception = ex;
                @out = null; return status;
            }
        }

        public Serializer_Status toCodingCategories  (string json, out ICodingCategoryList @out)    {
            Serializer_Status status = new Serializer_Status();

            try {
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var resp = JsonConvert.DeserializeObject<ICodingCategoryList>(json, set);
                @out = resp; return status;
            } catch (Exception ex) {
                status.Error = true;
                status.Exception = ex;
                @out = null; return status;
            }
        }

        public Serializer_Status toLanguage          (string json, out ILanguage @out)              {
            Serializer_Status status = new Serializer_Status();

            try {
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var resp = JsonConvert.DeserializeObject<ILanguage>(json, set);
                @out = resp; return status;
            } catch (Exception ex) {
                status.Error = true;
                status.Exception = ex;
                @out = null; return status;
            }
        }

        public Serializer_Status toLanguages         (string json, out ILanguageList @out)          {
            Serializer_Status status = new Serializer_Status();

            try {
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var resp = JsonConvert.DeserializeObject<ILanguageList>(json, set);
                @out = resp; return status;
            } catch (Exception ex) {
                status.Error = true;
                status.Exception = ex;
                @out = null; return status;
            }
        }

        public Serializer_Status toLivestreamInfo    (string json, out ILivestream @out)            {
            Serializer_Status status = new Serializer_Status();

            try {
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var resp = JsonConvert.DeserializeObject<ILivestream>(json, set);
                @out = resp; return status;
            } catch (Exception ex) {
                status.Error = true;
                status.Exception = ex;
                @out = null; return status;
            }
        }

        public Serializer_Status toLivestreams       (string json, out ILivestreamList @out)        {
            Serializer_Status status = new Serializer_Status();

            try {
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var resp = JsonConvert.DeserializeObject<ILivestreamList>(json, set);
                @out = resp; return status;
            } catch (Exception ex) {
                status.Error = true;
                status.Exception = ex;
                @out = null; return status;
            }
        }

        public Serializer_Status toUser              (string json, out IUser @out) {
            Serializer_Status status = new Serializer_Status();

            try {
                var set = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                var resp = JsonConvert.DeserializeObject<IUser>(json, set);
                @out = resp; return status;
            } catch(Exception ex) {
                status.Error     = true;
                status.Exception = ex;
                @out = null; return status;
            }
        }
    }
}
