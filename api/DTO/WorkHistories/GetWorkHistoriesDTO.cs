using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.WorkHistories
{
	public class GetWorkHistoriesDTO : GetEntityFilterDTO
	{

		[StringLength(16, MinimumLength = 16, ErrorMessage = "NIK harus memiliki 16 karakter")]
		[RegularExpression("^[0-9]{16}$", ErrorMessage = "NIK hanya berupa angka")]
		public string? NIK {  get; set; }

		public EmploymentStatus? Status { get; set; }

	}
}
