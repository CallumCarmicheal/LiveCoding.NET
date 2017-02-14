using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LiveEdu.API.Wrappers.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace LiveEdu.API.Wrappers {
    public class Serializer_Status {
        public Exception Exception;
        public bool      Error = false;
    }

    public class Serializer {
        public class JSONnetPrivateManager : Newtonsoft.Json.Serialization.DefaultContractResolver {
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

        public Serializer_Status toType<T>           (string json, out T @out) {
            Serializer_Status status = new Serializer_Status();

            try {
                var set = new JsonSerializerSettings() { ContractResolver = new JSONnetPrivateManager() };
                var resp = JsonConvert.DeserializeObject<T>(json, set);
                @out = resp; return status;
            } catch (Exception ex) {
                status.Error = true;
                status.Exception = ex;
                @out = default(T); return status;
            }
        }
    }
}
