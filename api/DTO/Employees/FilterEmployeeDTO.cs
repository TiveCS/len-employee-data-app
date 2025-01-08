using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.Employees
{
	public class FilterEmployeeDTO : GetEntityFilterDTO
	{

		[StringLength(16, MinimumLength = 16, ErrorMessage = "NIK harus memiliki 16 karakter")]
		[RegularExpression("^[0-9]{16}$", ErrorMessage = "NIK hanya berupa angka")]
		public string? NIK { get; set; }

		public string? Name { get; set; }

		public Gender? Gender { get; set; }

		public string? BirthCity { get; set; }

		[DataType(DataType.Date)]
		public DateTime? Birthday { get; set; }

		[MinLength(1)]
		public ICollection<int>? EmploymentLevelId { get; set; }

		[MinLength(1)]
		public ICollection<int>? UnitId { get; set;}

	}
}
