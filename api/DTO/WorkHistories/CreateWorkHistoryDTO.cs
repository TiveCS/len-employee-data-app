﻿using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.WorkHistories
{
	public class CreateWorkHistoryDTO
	{

		[Required]
		public required string EmployeeId { get; set; }

		[Required]
		public required string Company {  get; set; }

		[Required]
		[EnumDataType(typeof(EmploymentStatus))]
		public EmploymentStatus Status { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime StartDate {  get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime EndDate { get; set; }

	}
}
