namespace API.helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int totalPages, int totalItems, int CurrentPage, int totalItemPerPage)
        {
            this.totalPages = totalPages;
            this.totalItems = totalItems;
            this.CurrentPage = CurrentPage;
            this.totalItemPerPage = totalItemPerPage;
        }


        public int totalItemPerPage { get; set; }
        public int totalPages { get; set; }

        public int totalItems { get; set; }

        public int CurrentPage { get; set; }
    }
}