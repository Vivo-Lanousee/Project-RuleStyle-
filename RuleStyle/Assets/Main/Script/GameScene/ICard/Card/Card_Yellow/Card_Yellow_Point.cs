using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���_���擾����B
/// </summary>
public class Card_Yellow_Point : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    Card_Pattern ICard.card_pattern => Card_Pattern.Orange;

    string ICard.CardName => "���_�J�[�h";

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
