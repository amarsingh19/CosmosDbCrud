using Newtonsoft.Json;
using System.Collections.Generic;

namespace Students.Domain
{
    public class Student
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public Institute Institute;
        public Address Address;
        public string FirstName;
        public string LastName;
        public List<Course> Courses;
    }
}
