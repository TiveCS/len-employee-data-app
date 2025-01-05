using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
	// Jabatan Karyawan
	[Table("employment_levels")]
	public class EmploymentLevel
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public virtual ICollection<Employee> Employees { get; set; } = [];

	}
}
