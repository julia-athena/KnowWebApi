namespace WebApp.Models;

public class PageOptions
{
    public int Page { get; set; }
    public int PageSize { get; private set; } = 10;
    
    
}