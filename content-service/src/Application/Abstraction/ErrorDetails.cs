using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Application.Abstraction
{
    public class ErrorDetails
    {
        public int Status { get; private set; }
        public string Message { get; private set; }

        public ErrorDetails(int status, string message)
        {
            Status = status;
            Message = message;
        }
        public override string ToString()
        {
            JsonSerializerSettings settings = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };
            return JsonConvert.SerializeObject(this, settings);
        }
    }
}
