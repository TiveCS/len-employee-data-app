using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
	public class GetEntityFilterDTO
	{

		public int? Limit { get; set; } = 100;

		public int? Page { get; set; } = 1;

	}
}
