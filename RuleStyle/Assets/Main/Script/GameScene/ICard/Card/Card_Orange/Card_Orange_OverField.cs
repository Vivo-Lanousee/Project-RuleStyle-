using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��X�e�[�W�O�ɏo���Ƃ��ɔ�������
/// </summary>
public class Card_Orange_OverField : ICard
{

    public PlayerSessionData PlayerData { get; set; } = null;

    Card_Pattern ICard.card_pattern => Card_Pattern.Orange;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "��O��";

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {

    }
}
