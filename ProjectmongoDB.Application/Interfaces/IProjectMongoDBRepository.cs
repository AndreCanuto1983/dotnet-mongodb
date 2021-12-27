using MongoDB.Bson;
using ProjectmongoDB.Application.Model;
using ProjectmongoDB.Application.Model.Response;

namespace ProjectmongoDB.Application.Interfaces
{
    public interface IProjectMongoDBRepository
    {
        Task<List<PersonModel>> GetAllDataAsync(CancellationToken cancellationToken);
        Task<PersonResponse> GetByIdAsync(ObjectId personId, CancellationToken cancellationToken);
        Task<PersonResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
        Task InsertAsync(PersonModel personModel, CancellationToken cancellationToken);
        Task UpdateAsync(PersonModel personModel, CancellationToken cancellationToken);
        Task DeleteAsync(string cpf, CancellationToken cancellationToken);
    }
}
