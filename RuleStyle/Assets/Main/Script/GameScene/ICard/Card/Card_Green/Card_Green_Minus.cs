using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v�Z���@�F����
/// </summary>
public class Card_Green_Minus : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => 40;
    Card_Pattern ICard.card_pattern => Card_Pattern.Yellow;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "-";



    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {

    }
}
