using api.Data;
using api.Models;

namespace api.Repositories
{
	public class EmployeesWorkHistoriesRepository : IRepository<EmployeeWorkHistory, string>
	{

		private readonly EmployeesDbContext _context;

		public EmployeesWorkHistoriesRepository(EmployeesDbContext context) {
			_context = context;
		}

		public Task<string> CreateAsync(EmployeeWorkHistory entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<EmployeeWorkHistory>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<EmployeeWorkHistory> GetByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateByIdAsync(string id, EmployeeWorkHistory entity)
		{
			throw new NotImplementedException();
		}
	}
}
