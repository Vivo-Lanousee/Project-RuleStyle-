using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��m���Ԃ��������ɔ�������
/// </summary>
public class Card_Orange_Attack : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    Card_Pattern ICard.card_pattern => Card_Pattern.Orange;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�����̋�ɓ����邱�Ƃ�";

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {

    }
}
