﻿using api.Models;

namespace api.DTO.WorkUnits
{
	public class WorkUnitDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public void FromEntity(WorkUnit unit)
		{
			Id = unit.Id;
			Name = unit.Name;
		}
	}
}