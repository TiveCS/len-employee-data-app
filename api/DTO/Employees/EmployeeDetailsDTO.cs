using api.DTO.EmploymentLevels;
using api.DTO.WorkHistories;
using api.DTO.WorkUnits;
using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.Employees
{
	public class EmployeeDetailsDTO
	{

		public string Id { get; set; }
		public string Name { get; set; }
		public Gender Gender { get; set; }
		public string BirthCity { get; set; }
		public DateTime Birthday { get; set; }
		public IEnumerable<EmployeeWorkHistoryDTO> WorkHistories { get; set; }
		public EmploymentLevelDTO EmploymentLevel { get; set; }
		public WorkUnitDTO Unit { get; set; }


		public void FromEntity(Employee employee)
		{
			Id = employee.Id;
			Name = employee.Name;
			Gender = employee.Gender;
			BirthCity = employee.BirthCity;
			Birthday = employee.Birthday;

			Unit = new()
			{
				Id = employee.Unit.Id,
				Name = employee.Unit.Name,
			};

			EmploymentLevel = new() 
			{ 
				Id = employee.EmploymentLevel.Id,
				Name = employee.EmploymentLevel.Name
			};

			WorkHistories = employee.WorkHistories.Select(wh => new EmployeeWorkHistoryDTO 
			{
				Id = wh.Id,
				Company = wh.Company,
				Status = wh.Status,
				StartDate = wh.StartDate,
				EndDate = wh.EndDate,
			});
		}
	}
}
