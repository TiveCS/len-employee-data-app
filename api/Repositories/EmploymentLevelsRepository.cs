using api.Data;
using api.DTO.EmploymentLevels;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
	public class EmploymentLevelsRepository : IRepository<EmploymentLevel, int>
	{

		private readonly EmployeesDbContext _context;
		
		public EmploymentLevelsRepository(EmployeesDbContext context) {
			_context = context;
		}

		public async Task<int> CreateAsync(EmploymentLevel entity)
		{
			await _context.EmploymentLevels.AddAsync(entity);

			await _context.SaveChangesAsync();

			return entity.Id;
		}

		public async Task DeleteByIdAsync(int id)
		{
			await _context.EmploymentLevels.Where(level => level.Id.Equals(id)).ExecuteDeleteAsync();
		}

		public async Task<IEnumerable<EmploymentLevel>> GetAllAsync()
		{
			return await _context.EmploymentLevels.ToListAsync();
		}

		public async Task<IEnumerable<EmploymentLevel>> GetManyAsync(FilterEmploymentLevelDTO filter)
		{
			IQueryable<EmploymentLevel> query = _context.EmploymentLevels;

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

			if (!string.IsNullOrEmpty(filter.Name))
			{
				query = query.Where(e => e.Name.ToLower().Contains(filter.Name.ToLower()));
			}

			return await query.ToListAsync();
		}


		public async Task<EmploymentLevel> GetByIdAsync(int id)
		{
			return await _context.EmploymentLevels.Where(level => level.Id.Equals(id)).FirstAsync();
		}

		public async Task UpdateByIdAsync(int id, EmploymentLevel entity)
		{
			var level = await _context.EmploymentLevels.Where(level => level.Id.Equals(id)).FirstAsync();

			level.Name = entity.Name;

			await _context.SaveChangesAsync();
		}

		public async Task<int> CountAysnc()
		{
			return await _context.EmploymentLevels.CountAsync();
		}
	}
}
