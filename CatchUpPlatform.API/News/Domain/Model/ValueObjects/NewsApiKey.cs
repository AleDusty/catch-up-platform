namespace CatchUpPlatform.API.News.Domain.Model.ValueObjects;

public record NewsApiKey
{
    private const int MaxLength = 255;

    public NewsApiKey(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
           throw new ArgumentException("NewsApiKey cannot be null or whitespace", nameof(value));
           
        if(value.Length > MaxLength)
            throw new ArgumentException("NewsApiKey cannot be longer than {MaxLength} characters", nameof(value));
        
        Value = value;
    }
    
    public string Value { get; }
    public override string ToString() => Value;
    
    public string MaskedForLogging()=>MaskedForLogging(Value);

    public static string MaskedForLogging(string value) => 
        string.IsNullOrWhiteSpace(value) 
            ? string.Empty 
            : value.Length<=4
            ? new string('*', value.Length)
            : new string('*', value.Length - 4) + value[^4..];
}