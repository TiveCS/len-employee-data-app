using api.Data;
using api.DTO.WorkUnits;
using api.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace api.Repositories
{
	public class WorkUnitsRepository : IRepository<WorkUnit, int>
	{

		private readonly EmployeesDbContext _context;

		public WorkUnitsRepository(EmployeesDbContext context)
		{
			_context = context;
		}

		public async Task<int> CreateAsync(WorkUnit entity)
		{
			await _context.WorkUnits.AddAsync(entity);

			await _context.SaveChangesAsync();

			return entity.Id;
		}

		public async Task DeleteByIdAsync(int id)
		{
			await _context.WorkUnits.Where(u => u.Id.Equals(id)).ExecuteDeleteAsync();
		}

		public async Task<IEnumerable<WorkUnit>> GetAllAsync()
		{
			return await _context.WorkUnits.ToListAsync();
		}

		public async Task<IEnumerable<WorkUnit>> GetManyAsync(FilterWorkUnitDTO filter)
		{
			IQueryable<WorkUnit> query = _context.WorkUnits;

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

		public async Task<WorkUnit> GetByIdAsync(int id)
		{
			return await _context.WorkUnits.Where(u => u.Id.Equals(id)).FirstAsync();
		}

		public async Task UpdateByIdAsync(int id, WorkUnit entity)
		{
			var unit = await _context.WorkUnits.Where(u => u.Id.Equals(id)).FirstAsync();

			unit.Name = entity.Name;

			await _context.SaveChangesAsync();
		}

		public async Task<int> CountAysnc()
		{
			return await _context.WorkUnits.CountAsync();
		}
	}
}
