﻿using System.ComponentModel.DataAnnotations;

namespace web.Models
{
	public enum EmploymentStatus
	{
		Fulltime = 1,
		Contract = 2,
	}

	public class EmployeeWorkHistory
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string Company { get; set; }

		public EmploymentStatus Status { get; set; }

		[DataType(DataType.Date)]
		public DateTime StartDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime EndDate { get; set; }

		public string EmployeeId { get; set; }

		public virtual Employee Employee { get; set; }
	}
}