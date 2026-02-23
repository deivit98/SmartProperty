namespace SmartProperty.AI.Worker.Models;

/// <summary>
/// A single property match from semantic search.
/// </summary>
/// <param name="Id">Property id.</param>
/// <param name="Title">Property title.</param>
/// <param name="Description">Optional description.</param>
/// <param name="CreatedAt">UTC creation time.</param>
/// <param name="Score">Similarity score from vector search (0–1, higher is more similar).</param>
public record PropertySearchMatch(Guid Id, string Title, string? Description, DateTime CreatedAt, float Score);
