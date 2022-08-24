using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectmongoDB.Application.Convert;
using ProjectmongoDB.Application.Interfaces;
using ProjectmongoDB.Application.Model;
using ProjectmongoDB.Application.Model.Configuration;
using ProjectmongoDB.Application.Model.Response;

namespace ProjectmongoDB.Infra.Repository
{
    public class ProjectMongoDBRepository : IProjectMongoDBRepository
    {
        private readonly IOptions<ConfigurationDB> _options;
        private readonly ILogger<ProjectMongoDBRepository> _logger;
        private readonly IMongoCollection<PersonModel> _mongoCollection;

        public ProjectMongoDBRepository(
            IOptions<ConfigurationDB> options,
            ILogger<ProjectMongoDBRepository> logger)
        {
            _options = options;
            _logger = logger;

            var mongoClient = new MongoClient(_options.Value.ConnectionString);
            var database = mongoClient.GetDatabase(_options.Value.NameDB);

            _mongoCollection = database.GetCollection<PersonModel>(_options.Value.CollectionName);
        }

        public async Task<List<PersonModel>> GetAllDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _mongoCollection.FindAsync(x => x.Active).Result.ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("[ProjectMongoDBService][GetAllDataAsync] => EXCEPTION: {ex.Message}", ex.Message);
                throw;
            }
        }

        public async Task<PersonResponse> GetByIdAsync(ObjectId personId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mongoCollection.FindAsync(e => e.Id.Equals(personId)).Result.SingleOrDefaultAsync(cancellationToken);

                if (response != null)
                    return response.ToPersonResponseConvert();

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ProjectMongoDBService][GetByIdAsync] => EXCEPTION: {ex.Message}", ex.Message);
                throw;
            }
        }

        public async Task<PersonResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mongoCollection.FindAsync(e => e.Cpf.Contains(cpf)).Result.SingleOrDefaultAsync(cancellationToken);

                if (response != null)
                    return response.ToPersonResponseConvert();

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ProjectMongoDBService][GetByCpfAsync] => EXCEPTION: {ex.Message}", ex.Message);
                throw;
            }
        }

        public async Task InsertAsync(PersonModel personModel, CancellationToken cancellationToken)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(personModel, null, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ProjectMongoDBService][InsertAsync]");
                throw;
            }
        }

        public async Task UpdateAsync(PersonModel personModel, CancellationToken cancellationToken)
        {
            try
            {
                await _mongoCollection.ReplaceOneAsync(
                    x => x.Cpf == personModel.Cpf,
                    personModel)
                    .WaitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ProjectMongoDBService][UpdateAsync]");
                throw;
            }
        }

        public async Task DeleteAsync(string cpf, CancellationToken cancellationToken)
        {
            try
            {
                await _mongoCollection.FindOneAndDeleteAsync(x => x.Cpf == cpf, null, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ProjectMongoDBService][DeleteAsync]");
                throw;
            }
        }
    }
}
