using api.Data;
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
	}
}
