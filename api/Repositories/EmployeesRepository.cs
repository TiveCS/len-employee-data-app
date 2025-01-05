using api.Data;
using api.DTO.Employees;
using api.DTO.EmploymentLevels;
using api.DTO.WorkHistories;
using api.DTO.WorkUnits;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace api.Repositories
{
	public class EmployeesRepository : IRepository<Employee, string>
	{
		private readonly EmployeesDbContext _context;

		public EmployeesRepository(EmployeesDbContext context) {
			_context = context;
		}

		public async Task<string> CreateAsync(Employee entity)
		{
			await _context.Employees.AddAsync(entity);

			await _context.SaveChangesAsync();

			return entity.Id;
		}

		public async Task DeleteByIdAsync(string id)
		{
			await _context.Employees.Where(e => e.Id.Equals(id)).ExecuteDeleteAsync();
		}

		public async Task<IEnumerable<Employee>> GetAllAsync()
		{
			return await _context.Employees.ToListAsync();
		}

		public async Task<Employee> GetByIdAsync(string id)
		{
			return await _context.Employees.Where(e => e.Id.Equals(id))
				.Include(e => e.Unit)
				.Include(e => e.EmploymentLevel)
				.Include(e => e.WorkHistories)
				.FirstAsync();
		}

		public async Task UpdateByIdAsync(string id, Employee entity)
		{
			var employee = await _context.Employees.Where(e => e.Id.Equals(id)).FirstAsync();

			employee.Name = entity.Name;
			employee.BirthCity = entity.BirthCity;
			employee.Birthday = entity.Birthday;
			employee.Gender = entity.Gender;
			employee.EmploymentLevelId = entity.EmploymentLevelId;
			employee.UnitId = entity.UnitId;

			await _context.SaveChangesAsync();
		}
	}
}
