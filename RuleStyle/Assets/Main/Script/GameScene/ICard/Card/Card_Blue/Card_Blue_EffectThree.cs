using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Blue_EffectThree : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "P3��";

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {
        PlayerData.EffectPlayer_Id.Add(3);
    }
}
