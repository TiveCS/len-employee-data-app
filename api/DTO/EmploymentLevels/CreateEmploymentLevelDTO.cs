using System.ComponentModel.DataAnnotations;

namespace api.DTO.EmploymentLevels
{
	public class CreateEmploymentLevelDTO
	{

		[Required]
		public required string Name { get; set; }
	}
}
