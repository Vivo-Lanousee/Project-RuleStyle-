
/// <summary>
/// ���σJ�[�h���Q�Ƃ���
/// </summary>
public class Card_Yellow_CardDraw : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => 25;
    Card_Pattern ICard.card_pattern => Card_Pattern.Yellow;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�J�[�h������";

    void ICard.CardNum()
    {
        foreach(var data in PlayerData.EffectAwardPlayer_Id)
        {
            PlayerData
                .gameSessionManager
                .DeckDraw(PlayerData, PlayerData.RuleSuccessNum);
        }
    }
}
