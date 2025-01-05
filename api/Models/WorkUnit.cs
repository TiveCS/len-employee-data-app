using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
	// Unit Kerja Karyawan
	[Table("work_units")]
	public class WorkUnit
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public virtual ICollection<Employee> Employees { get; set; } = [];
	}
}
