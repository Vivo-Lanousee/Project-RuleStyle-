using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Blue_EffectFour : ICard,ICard_Blue
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => 50;

    

    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "P4��";

    /// <summary>
    /// �J�[�hBlue�̎��݂̂̎����ƂȂ�B
    /// </summary>
    public List<int> EffectMember => new List<int>{4};

    void ICard.CardNum()
    {
        //PlayerData.EffectPlayer_Id.Add(4);
    }
}