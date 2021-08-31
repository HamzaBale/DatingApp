namespace API.helpers
{
    public class UserParams : PaginationParams
    {
    
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