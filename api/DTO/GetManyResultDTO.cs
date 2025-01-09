namespace api.DTO
{
	public class GetManyResultDTO<TEntity>
	{
		
		public required IEnumerable<TEntity> Entities { get; set; }

		public required int TotalCount { get; set; }
	}
}
