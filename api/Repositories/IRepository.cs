namespace api.Repositories
{
	public interface IRepository<TEntity, TKey> where TEntity : class
	{

		public Task<TKey> CreateAsync(TEntity entity);

		public Task UpdateByIdAsync(TKey id, TEntity entity);

		public Task DeleteByIdAsync(TKey id);

		public Task<TEntity> GetByIdAsync(TKey id);

		public Task<IEnumerable<TEntity>> GetAllAsync();

	}
}
