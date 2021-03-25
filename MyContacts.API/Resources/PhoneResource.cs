using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyContacts.API.Resources
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PhoneType : byte
    {
        [EnumMember(Value = "home")]
        Home,
        [EnumMember(Value = "work")]
        Work,
        [EnumMember(Value = "mobile")]
        Mobile
    }

    public class PhoneResource
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }
    }
}
