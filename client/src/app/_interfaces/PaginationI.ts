
export class pagination{
    totalItems:number;
    totalItemsPerPage:number;
    CurrentPage:number;
    totalPages:number;
}

export class PaginatedResult<T>{
        result:T; // Members[]
        pagination:pagination;
}