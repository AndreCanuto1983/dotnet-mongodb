using MongoDB.Bson;

namespace ProjectmongoDB.Application.Model.Request
{
    public class PersonUpdateRequest
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public bool Active { get; set; }

        public bool IsValid()
        {
            return (
                string.IsNullOrEmpty(Id) && 
                ObjectId.Parse(Id).Equals(true) &&
                string.IsNullOrEmpty(Name) &&
                string.IsNullOrEmpty(Cpf) &&
                string.IsNullOrEmpty(Phone) &&
                Birthday >= DateTime.MinValue && Birthday <= DateTime.MaxValue);
        }
    }
}