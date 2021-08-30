namespace API.helpers
{
    public class UserParams
    {
        public int pageNumber { get; set; } = 1;
        public int MinAge { get; set; } = 18;

        private const int MaxSize= 15;
         private int _pageSize = 2;
        
        public int pageSize { 
            get => _pageSize;
            set =>  _pageSize = (MaxSize > value) ? value : MaxSize;  
          } 
        
        public string CurrentUsername { get; set; }

        public  string Gender { get; set; }
        
        public int FromAge { 
            get => MinAge; 
            set => MinAge = (MinAge > value) ? MinAge : value;  
            }
             public int ToAge { 
            get => MaxAge; 
            set => MaxAge = (MaxAge > value) ? value : MaxAge;  
            }

        public int MaxAge { get; set; } = 140;



    }
}