using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card_Purple_Three : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    /// <summary>
    /// ��J�[�h�̈�Null
    /// </summary>
    public float? ProbabilityNum => 3;
    Card_Pattern ICard.card_pattern => Card_Pattern.Purple;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "3";
    Sprite ICard.cardUI { get; set; }
    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {
        if (PlayerData != null)
        {
            PlayerData.RuleSuccessNum = 3;
        }
    }
}
