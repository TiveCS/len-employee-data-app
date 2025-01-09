using api.Data;
using api.DTO.WorkHistories;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
	public class EmployeesWorkHistoriesRepository : IRepository<EmployeeWorkHistory, string>
	{

		private readonly EmployeesDbContext _context;

		public EmployeesWorkHistoriesRepository(EmployeesDbContext context) {
			_context = context;
		}

		public async Task<string> CreateAsync(EmployeeWorkHistory entity)
		{
			await _context.AddAsync(entity);

			await _context.SaveChangesAsync();

			return entity.Id;
		}

		public async Task DeleteByIdAsync(string id)
		{
			await _context.WorkHistories.Where(e => e.Id == id).ExecuteDeleteAsync();
		}

		public async Task<IEnumerable<EmployeeWorkHistory>> GetAllAsync()
		{
			return await _context.WorkHistories.Include(e => e.Employee).ToListAsync();
		}

		public async Task<IEnumerable<EmployeeWorkHistory>> GetManyAsync(FilterWorkHistoryDTO filter)
		{
			IQueryable<EmployeeWorkHistory> query = _context.WorkHistories
				.Include(e => e.Employee);

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

			if (!string.IsNullOrEmpty(filter.Company)) {
				query = query.Where(e => e.Company.ToLower().Contains(filter.Company.ToLower()));
			}

			if (filter.StartDate.HasValue)
			{
				query = query.Where(e => e.StartDate <= filter.StartDate.Value);
			}

			if (filter.EndDate.HasValue)
			{
				query = query.Where(e => e.EndDate <= filter.EndDate.Value);
			}

			if (filter.Status.HasValue)
			{
				query = query.Where(e => e.Status == filter.Status.Value);
			}

			return await query.ToListAsync();
		}

		public async Task<EmployeeWorkHistory> GetByIdAsync(string id)
		{
			return await _context.WorkHistories
				.Include(e => e.Employee)
				.Where(e => e.Id == id).FirstAsync();
		}

		public async Task UpdateByIdAsync(string id, EmployeeWorkHistory entity)
		{
			var history = await _context.WorkHistories.Where(e => e.Id == id).FirstAsync();

			history.Company = entity.Company;
			history.Status = entity.Status;
			history.StartDate = entity.StartDate;
			history.EndDate = entity.EndDate;

			await _context.SaveChangesAsync();
		}

		public async Task<int> CountAysnc()
		{
			return await _context.WorkHistories.CountAsync();
		}
	}
}
