using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBol.Models.MongoDb
{
    public class CONTAINERS
    {
        public string? ContainerID { get; set; }
        public string? ContainerCode { get; set; }
        public string? ContainerDescription { get; set; }
        public int Weight { get; set; }


    }
}
