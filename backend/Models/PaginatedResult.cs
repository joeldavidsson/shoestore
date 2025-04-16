namespace Backend.Models;

public class PaginatedResult<T>
{
  public IEnumerable<T> Items { get; set; } = [];
  public int TotalCount { get; set; }
  public int CurrentPage { get; set; }
  public int PageSize { get; set; }
}