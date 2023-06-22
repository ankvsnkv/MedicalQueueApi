using Newtonsoft.Json;

namespace MedicalQueueApi.Models
{
    public class ExampleModel
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string Login { get; set; }
    }
}
