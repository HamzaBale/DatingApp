namespace API.Extensions
{
    public static class IntExtensions
    {
        public static bool isInt(this int num){
                if(num.GetType() == typeof(int)) return true;
                else return false;
        }
    }
}