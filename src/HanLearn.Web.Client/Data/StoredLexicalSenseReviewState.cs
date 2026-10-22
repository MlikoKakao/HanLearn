namespace HanLearn.Web.Client.Data;

using System.Text.Json.Serialization;

// Done this because Serialize/deserialize doesn't do it correctly for Card
// Need this helper card to translate from-to browser storage
public record StoredLexicalSenseReviewState
{
    // Keep existing browser data and exported backups compatible.
    [JsonPropertyName("VocabularyEntryId")]
    public required string LexicalSenseId { get; init; }
    public required StoredCard Card { get; set; }
}
