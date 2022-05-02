namespace ContosoUniversityApi.Filter
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public PaginationFilter()
        {
        }
        public PaginationFilter(string pageNumber, string pageSize)
        {
            int number;
            int size;

            if (int.TryParse(pageNumber, out number) && int.TryParse(pageSize, out size))
            {
                this.PageNumber = number < 1 ? 1 : number;
                this.PageSize = size > 20 ? 20 : size;
            }
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 20 ? 20 : pageSize;
        }
    }
}