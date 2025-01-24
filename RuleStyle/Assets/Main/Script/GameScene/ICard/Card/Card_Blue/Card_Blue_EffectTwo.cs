using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Blue_EffectTwo : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => 50;
    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "P2��";

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {
        PlayerData.EffectPlayer_Id.Add(2);
    }
}
