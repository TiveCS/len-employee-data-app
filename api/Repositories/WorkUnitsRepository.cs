using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

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
	}
}
