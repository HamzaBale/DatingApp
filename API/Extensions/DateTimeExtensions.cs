using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob){
            var today = DateTime.Today;
            int age = today.Year - dob.Year;
            if(dob.Day < today.Day) age--;
            return age; 
        }
    }
}