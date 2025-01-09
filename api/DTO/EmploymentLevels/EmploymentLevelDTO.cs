using api.Models;

namespace api.DTO.EmploymentLevels
{
	public class EmploymentLevelDTO
	{

		public int Id { get; set; }
		public string Name { get; set; }


		public EmploymentLevelDTO FromEntity(EmploymentLevel level)
		{
			Id = level.Id;
			Name = level.Name;

			return this;
		}

	}
}
