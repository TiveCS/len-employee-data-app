using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.WorkHistories
{
	public class FilterWorkHistoryDTO : GetEntityFilterDTO
	{

		public ICollection<int>? EmployeesId { get; set; }

		public EmploymentStatus? Status {  get; set; }

		[DataType(DataType.Date)]
		public DateTime? StartDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime? EndDate { get; set; }

		public string? Company {  get; set; }
	}
}
