namespace HanLearn.Web.Client.Data;



// Done this because Serialize/deserialize doesn't do it correctly for Card
// Need this helper card to translate from-to browser storage
public record StoredVocabularyReviewState
{
    public required string VocabularyEntryId { get; init; }
    public required StoredCard Card { get; set; }
}
