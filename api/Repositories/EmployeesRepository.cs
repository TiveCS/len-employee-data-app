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

		public async Task<IEnumerable<Employee>> GetManyAsync(FilterEmployeeDTO filter)
		{
			IQueryable<Employee> query = _context.Employees.OrderBy(e => e.Name);

			var hasLimit = filter.Limit.HasValue && filter.Limit.Value > 0;
			var hasPagination = filter.Page.HasValue && filter.Page.Value > 0;

			if (hasLimit)
			{
				query = query.Take(filter.Limit!.Value);
			}

			if (hasPagination)
			{
				query = query.Skip((filter.Page!.Value - 1) * filter.Limit!.Value);
			}

			if (!string.IsNullOrEmpty(filter.NIK)) {
				query = query.Where(e => e.NIK.Equals(filter.NIK));
			}

			if (!string.IsNullOrEmpty(filter.Name))
			{
				query = query.Where(e => e.Name.ToLower().Contains(filter.Name.ToLower()));
			}

			if (filter.Gender != null)
			{
				query = query.Where(e => e.Gender == filter.Gender);
			}

			if (filter.EmploymentLevelId != null && filter.EmploymentLevelId.Count > 0) {
				query = query.Where(e => filter.EmploymentLevelId.Contains(e.EmploymentLevelId));
			}

			if (filter.UnitId != null && filter.UnitId.Count > 0)
			{
				query = query.Where(e => filter.UnitId.Contains(e.UnitId));
			}

			if (!string.IsNullOrEmpty(filter.BirthCity)) { 
				query = query.Where(e => filter.BirthCity.Contains(e.BirthCity));
			}

			if (filter.Birthday.HasValue)
			{
				query = query.Where(e => e.Birthday.Equals(filter.Birthday));
			}

			return await query.ToListAsync();
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

		public async Task<int> CountAysnc()
		{
			return await _context.Employees.CountAsync();
		}
	}
}
