namespace InTech_MVC.Helpers
{
    public class PaginationList<T> : List<T>
    {
        public PaginationList(List<T> values, int count, int page, int pageSize)
        {
            this.AddRange(values);
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            Page = page;
        }

        public int TotalPage { get; set; }
        public int Page { get; set; }
        public bool HasPrevious { get => Page > 1; }
        public bool HasNext { get => Page < TotalPage; }



        public static PaginationList<T> Create(IQueryable<T> query, int pageSize, int page)
        {
            return new PaginationList<T>(query.Skip((page - 1) * pageSize).Take(pageSize).ToList(), query.Count(), page, pageSize);
        }
    }
}
