namespace Cosmos.Apis.Services
{
    #region <Usings>
    using Microsoft.Azure.Cosmos;
    using Students.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    #endregion

    public class CosmosDbService : IDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }

        public async Task<Student> GetStudentAsync(string id)
        {
            try
            {
                ItemResponse<Student> response 
                    = await this._container.ReadItemAsync<Student>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            if(student == null || string.IsNullOrEmpty(student.Id))
            {
                throw new System.FormatException("Missing student data");
            }

            try
            {
                ItemResponse<Student> response =
                    await this._container
                                .CreateItemAsync<Student>(student, new PartitionKey(student.Id));

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            if (student == null || string.IsNullOrEmpty(student.Id))
            {
                throw new System.FormatException("Missing student data");
            }

            try
            {
                ItemResponse<Student> response =
                    await this._container
                                .UpsertItemAsync<Student>(student, new PartitionKey(student.Id));

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<bool> DeleteStudentAsync(string id)
        {
            ItemResponse<Student> response = await this._container.DeleteItemAsync<Student>(id, new PartitionKey(id));
            return (response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var query = this._container.GetItemQueryIterator<Student>(
                new QueryDefinition("SELECT * FROM c"));
            List<Student> students = new List<Student>();
            while(query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                students.AddRange(response.ToList());
            }

            return students;
        }
    }
}
