using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���_���擾����B
/// </summary>
public class Card_Yellow_Point : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => null;
    Card_Pattern ICard.card_pattern => Card_Pattern.Yellow;

    string ICard.CardName => "���_";
    Image ICard.cardUI { get; set; }
    /// <summary>
    /// PlayerData
    /// </summary>
    void ICard.CardNum()
    {
        if (PlayerData!=null)
        {
            
        }
    }
}
