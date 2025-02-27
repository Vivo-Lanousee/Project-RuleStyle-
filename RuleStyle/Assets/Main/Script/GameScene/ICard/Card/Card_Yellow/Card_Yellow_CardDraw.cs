using UnityEngine.UI;

using UnityEngine;
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
    Sprite ICard.cardUI { get; set; }
    void ICard.CardNum()
    {
        GameSessionManager manage = GameSessionManager.Instance();
        foreach (var data in PlayerData.EffectAwardPlayer_Id)
        {
            manage.DeckDraw(manage.Session_Data[data], PlayerData.RuleSuccessNum);
        }
    }
}
