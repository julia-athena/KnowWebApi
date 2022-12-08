namespace IntermediateLayer.IQueryableExtensions;

public static class Paging
{
    public static IQueryable<T> GetPaged<T>(this IQueryable<T> query, 
        int pageNumber, int pageSize) where T : class
    {
        var skip = (pageNumber - 1) * pageSize;
        return query
            .Skip(skip)
            .Take(pageSize);
    }
}