using MongoDB.Bson;
using ProjectmongoDB.Application.Model;
using ProjectmongoDB.Application.Model.Request;
using ProjectmongoDB.Application.Model.Response;

namespace ProjectmongoDB.Application.Convert
{
    public static class PersonConvert
    {
        public static PersonModel ToPersonInsertModelConvert(this PersonInsertRequest personRequest)
            => new()
            {                
                Name = personRequest.Name,
                Cpf = personRequest.Cpf,
                Phone = personRequest.Phone,
                Birthday = personRequest.Birthday,
                Active = personRequest.Active
            };

        public static PersonModel ToPersonUpdateModelConvert(this PersonUpdateRequest personRequest)
            => new()
            {             
                Id = ObjectId.Parse(personRequest.Id),
                Name = personRequest.Name,
                Cpf = personRequest.Cpf,
                Phone = personRequest.Phone,
                Birthday = personRequest.Birthday,
                Active = personRequest.Active
            };

        public static PersonResponse ToPersonResponseConvert(this PersonModel personModel)
            => new()
            {
                Name = personModel.Name,
                Cpf = personModel.Cpf,
                Phone = personModel.Phone,
                Birthday = personModel.Birthday,
                Active = personModel.Active
            };
    }
}
