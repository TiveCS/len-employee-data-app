using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.Employees
{
	public class CreateEmployeeDTO
	{

		[Required]
		[StringLength(16, MinimumLength = 16, ErrorMessage = "NIK harus memiliki 16 karakter")]
		[RegularExpression("^[0-9]{16}$", ErrorMessage = "NIK hanya berupa angka")]
		public required string NIK { get; set; }

		[Required]
		public required string Name { get; set; }

		[Required]
		public required string BirthCity { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }

		[Required]
		[EnumDataType(typeof(Gender))]
		public Gender Gender { get; set; }

		[Required]
		public int EmploymentLevelId { get; set; }

		[Required]
		public int UnitId { get; set; }
	}
}
