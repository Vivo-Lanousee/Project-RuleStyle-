using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card_Purple_One : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    /// <summary>
    /// ��J�[�h�̈�Null
    /// </summary>
    public float? ProbabilityNum => null;
    Card_Pattern ICard.card_pattern => Card_Pattern.Purple;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "1";

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {
        if (PlayerData != null)
        {
            PlayerData.RuleSuccessNum = 1;
        }
    }
}
