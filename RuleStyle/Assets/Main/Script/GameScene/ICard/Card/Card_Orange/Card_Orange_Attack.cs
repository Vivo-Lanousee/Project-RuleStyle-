using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��m���Ԃ��������ɔ�������
/// </summary>
public class Card_Orange_Attack : ICard
{
    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="_playerSettingData"></param>
    public Card_Orange_Attack(PlayerSessionData _playerSettingData)
    {
        PlayerData = _playerSettingData;
    }

    PlayerSessionData PlayerData;

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
