namespace web.Utils
{
	public static class QueryBuilder
	{
		public static string BuildQueryString<T>(T filter) where T : class
		{
			var properties = typeof(T).GetProperties()
				.Where(p => p.GetValue(filter) != null)
				.Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(filter).ToString())}");

			var query = string.Join("&", properties);
			return string.IsNullOrEmpty(query) ? string.Empty : $"?{query}";
		}
	}
}
