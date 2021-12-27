namespace ProjectmongoDB.Application.Model.Response
{
    public class PersonResponse
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public bool Active { get; set; }
    }
}
