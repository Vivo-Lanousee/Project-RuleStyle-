using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// “¾“_‚ğæ“¾‚·‚éB
/// </summary>
public class Card_Yellow_Point : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => null;
    Card_Pattern ICard.card_pattern => Card_Pattern.Yellow;

    string ICard.CardName => "“¾“_";
    Sprite ICard.cardUI { get; set; }
    /// <summary>
    /// PlayerData
    /// </summary>
    void ICard.CardNum()
    {
        if (PlayerData!=null)
        {
            if (PlayerData.RuleSuccessCalculation=="+")
            {

                PlayerData.PlayerPoint += PlayerData.RuleSuccessNum;
            }
            else if(PlayerData.RuleSuccessCalculation=="-") {
                PlayerData.PlayerPoint -= PlayerData.RuleSuccessNum;
                if (PlayerData.PlayerPoint < 0) {  PlayerData.PlayerPoint = 0; }
            }
        }
    }
}
