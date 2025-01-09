namespace web.Models
{
	public class GetManyResult<TEntity>
	{

		public required IEnumerable<TEntity> Entities { get; set; }

		public required int TotalCount { get; set; }

	}
}
