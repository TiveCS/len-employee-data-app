using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.WorkHistories
{
	public class EmployeeWorkHistoryDTO
	{

		public string Id { get; set; }
		public string Company { get; set; }
		public EmploymentStatus Status { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public string EmployeeId { get; set; }

		public Employee? Employee { get; set; }


		public EmployeeWorkHistoryDTO FromEntity(EmployeeWorkHistory history)
		{
			Id = history.Id;
			Company = history.Company;
			Status = history.Status;
			StartDate = history.StartDate;
			EndDate = history.EndDate;
			EmployeeId = history.EmployeeId;
			Employee = history.Employee != null ? new Employee()
			{
				Id = history.EmployeeId,
				NIK = history.Employee.NIK,
				Name = history.Employee.Name,
				Birthday = history.Employee.Birthday,
				BirthCity = history.Employee.BirthCity,
				Gender = history.Employee.Gender,
				EmploymentLevelId = history.Employee.EmploymentLevelId,
				UnitId = history.Employee.UnitId,
			} : null; 

			return this;
		}

	}
}
