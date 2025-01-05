using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{

	public enum Gender
	{
		Male = 1,
		Female = 2,
	}

	[Table("employees")]
	public class Employee
	{

		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string NIK {  get; set; }

		public string Name { get; set; }
		
		public Gender Gender { get; set; }
		
		public string BirthCity { get; set; }
	
		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }

		public int EmploymentLevelId {  get; set; }

		public int UnitId {  get; set; }

		
		public virtual EmploymentLevel EmploymentLevel { get; set; }

		public virtual WorkUnit Unit { get; set; }

		public virtual ICollection<EmployeeWorkHistory> WorkHistories { get; set; } = [];
		

	}
}
