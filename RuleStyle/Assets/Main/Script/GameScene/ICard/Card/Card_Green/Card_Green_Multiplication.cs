using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Green_Multiplication : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    Card_Pattern ICard.card_pattern => Card_Pattern.Yellow;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�~";



    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {

    }
}
