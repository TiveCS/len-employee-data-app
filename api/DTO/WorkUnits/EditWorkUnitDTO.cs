using System.ComponentModel.DataAnnotations;

namespace api.DTO.WorkUnits
{
	public class EditWorkUnitDTO
	{

		[Required]
		public required string Name { get; set; }

	}
}
