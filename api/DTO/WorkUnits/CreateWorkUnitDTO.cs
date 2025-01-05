using System.ComponentModel.DataAnnotations;

namespace api.DTO.WorkUnits
{
	public class CreateWorkUnitDTO
	{

		[Required]
		public required string Name { get; set; }

	}
}
