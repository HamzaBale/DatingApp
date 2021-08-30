using System.Text.Json;
using API.helpers;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtensions
    {
            public static void addPaginationHeader(this HttpResponse response, int totalPages, int totalItems, int CurrentPage, int totalItemPerPage ){
                var pagHeader = new PaginationHeader(totalPages,totalItems,CurrentPage,totalItemPerPage);

                response.Headers.Add("pagination",JsonSerializer.Serialize(pagHeader));
                response.Headers.Add("Access-Control-Expose-Headers","pagination");
            }
    }
}