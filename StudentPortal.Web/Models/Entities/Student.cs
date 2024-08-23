using System.ComponentModel.DataAnnotations;
namespace StudentPortal.Web.Models.Entities
{
	public class Student
	{
		[Key]
        public Guid Id { get; set; }
		public required string Name { get; set; }
        public required string Email { get; set; }
		public required string Phone { get; set; }
        public bool Subscribed { get; set; }
    }
}
