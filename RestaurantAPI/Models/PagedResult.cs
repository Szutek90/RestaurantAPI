namespace RestaurantAPI.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }

        public PagedResult(List<T> items, int pageSize, int pageNumber, int totalItems)
        {
            Items = items;
            TotalPages = (int)Math.Ceiling(totalItems/(decimal)pageSize);
            ItemsFrom = pageSize*(pageNumber-1)+1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalItemsCount = totalItems;
        }
    }
}
