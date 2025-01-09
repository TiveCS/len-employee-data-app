namespace web.Models
{

	public enum Gender
	{
		Male = 1,
		Female = 2,
	}

	public class Employee
	{
		public string Id { get; set; }
		public string NIK { get; set; }
		public string Name { get; set; }
		public Gender Gender { get; set; }
		public DateTime Birthday { get; set; }
		public string BirthCity { get; set; }
		public int UnitId { get; set; }
		public int EmploymentLevelId { get; set; }

		public EmploymentLevel EmploymentLevel { get; set; }
		public WorkUnit Unit { get; set; }
		public ICollection<EmployeeWorkHistory> WorkHistories { get; set; }
	}
}
