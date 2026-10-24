namespace CatchUpPlatform.API.News.Domain.Model.ValueObjects;

public record SourceId
{
    private const int MaxLength = 255;

    public SourceId(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("NewsApiKey cannot be null or whitespace", nameof(value));
           
        if(value.Length > MaxLength)
            throw new ArgumentException("NewsApiKey cannot be longer than {MaxLength} characters", nameof(value));
        
        Value = value;
    }
    
    public string Value { get; }
    public override string ToString() => Value;
    
}