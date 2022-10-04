using System.Collections.Generic;

namespace Helpers
{
    public class Pagination<TModel>
    {
        const int MaxPageSize = 100;
        private int _pageSize;

        public Pagination()
        {
            Items = new List<TModel>();
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<TModel> Items { get; set; }
    }
}
