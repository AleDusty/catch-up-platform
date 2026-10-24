namespace CatchUpPlatform.API.Shared.Domain.Model;

/// <summary>
/// Defines properties to track the creation and last update timestamps for an entity.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// The date and time when the entity was created.
    /// </summary>
    DateTimeOffset CreatedAt { get; set; }


    /// <summary>
    /// The date and time when the entity was last updated.
    /// </summary>
    DateTimeOffset UpdatedAt { get; set; }
}