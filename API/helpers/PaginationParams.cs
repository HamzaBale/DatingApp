namespace API.helpers
{
    public class PaginationParams
    {
         public int pageNumber { get; set; } = 1;
        public int MinAge { get; set; } = 18;

        private const int MaxSize= 15;
         private int _pageSize = 2;
        
        public int pageSize { 
            get => _pageSize;
            set =>  _pageSize = (MaxSize > value) ? value : MaxSize;  
          } 
    }
}