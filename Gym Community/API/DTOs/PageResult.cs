namespace Gym_Community.API.DTOs
{
    public class PageResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
