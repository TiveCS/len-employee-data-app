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


		public void FromEntity(EmployeeWorkHistory history)
		{
			Id = history.Id;
			Company = history.Company;
			Status = history.Status;
			StartDate = history.StartDate;
			EndDate = history.EndDate;
		}

	}
}
