using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.helpers
{
    public class PageList<T> : List<T> //generic
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }   

        public int TotalCount { get; set; } //how many items are in this query.

public PageList(IEnumerable<T> items,int pageNumber, int count, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling((count / (double) pageSize) ); 
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

public static async Task<PageList<T>> CreateAsync(IEnumerable<T> source,int pageNumber,int pageSize ){

    var count =  source.Count();
    var items =  source.Skip((pageNumber - 1 )*pageSize).Take(pageSize).ToList();
    return new PageList<T>(items, pageNumber,count,pageSize);
}
    }
}