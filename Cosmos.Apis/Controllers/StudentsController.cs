namespace Cosmos.Apis.Controllers
{
    #region <Usings>
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Cosmos.Apis.Services;
    using Microsoft.AspNetCore.Mvc;
    using Students.Domain;
    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService dbService;

        public StudentsController(IDbService dbService)
        {
            this.dbService = dbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            IEnumerable<Student> students = await this.dbService.GetStudentsAsync();
            return students;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Student student = await this.dbService.GetStudentAsync(id);
            if(student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            Student newStudent = await this.dbService.CreateStudentAsync(student);
            return Ok(newStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Student student)
        {
            Student updatedStudent = await this.dbService.UpdateStudentAsync(student);
            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool success = await this.dbService.DeleteStudentAsync(id);
            if(!success)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
