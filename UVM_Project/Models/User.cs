using System.ComponentModel.DataAnnotations;

namespace UVM_Project.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLastname { get; set; }
        public string Email {  get; set; }
        public string Department { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
