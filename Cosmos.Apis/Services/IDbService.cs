namespace Cosmos.Apis.Services
{
    #region <Usings>
    using Students.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    #endregion

    public interface IDbService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(string id);
        Task<bool> DeleteStudentAsync(string id);
        Task<Student> CreateStudentAsync(Student student);
        Task<Student> UpdateStudentAsync(Student student);
    }
}
