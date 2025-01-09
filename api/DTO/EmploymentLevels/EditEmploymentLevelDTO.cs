using System.ComponentModel.DataAnnotations;

namespace api.DTO.EmploymentLevels
{
	public class EditEmploymentLevelDTO
	{
		[Required]
		public required string Name { get; set; }
	}
}
