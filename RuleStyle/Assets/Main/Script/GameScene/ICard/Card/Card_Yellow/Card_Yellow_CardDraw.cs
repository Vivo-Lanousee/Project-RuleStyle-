using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���σJ�[�h���Q�Ƃ���
/// </summary>
public class Card_Yellow_CardDraw : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    Card_Pattern ICard.card_pattern => Card_Pattern.Yellow;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�J�[�h������";

    

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {

    }
}
