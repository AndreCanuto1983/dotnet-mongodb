using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectmongoDB.Application.Model
{    
    public class PersonModel
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("cpf")]
        public string Cpf { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("birthday")]
        public DateTime Birthday { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }
    }
}