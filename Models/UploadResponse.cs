using Newtonsoft.Json;

namespace AlexandriaWrapper.Model
{
    public class UploadResponse
    {
        public string IsError { get; set; }
        public Body Body { get; set; }
        public string Status { get; set; }
        public string Key { get; set; }
    }

    public class Body
    {
        public string url { get; set; }
        public Field fields { get; set; }
    }

    public class Field
    {
        public string key { get; set; }
        public string AWSAccessKeyId { get; set; }

        [JsonProperty("x-amz-security-token")]
        public string XAmzSecurityToken { get; set; }
        public string policy { get; set; }
        public string signature { get; set; }
    }
}
