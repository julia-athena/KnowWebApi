namespace IntermediateLayer.KnowDto;

public class PostDtoToCreate
{
    public string Title { get; set; }
    public long CreatorId { get; set; }
    
    public string Content { get; set; }
    public ICollection<string> Tags { get; set; }
}