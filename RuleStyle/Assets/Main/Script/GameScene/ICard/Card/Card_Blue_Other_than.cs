using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ȊO
/// </summary>
public class Card_Blue_Other_than : ICard
{
    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="_playerSettingData"></param>
    public Card_Blue_Other_than(PlayerSessionData _playerSettingData)
    {
        PlayerData=_playerSettingData;
    }

    PlayerSessionData PlayerData;

    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;
    
    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "Other_than";

    /// <summary>
    /// �͑S�ĕԂ�l�Ō��ʂ��s��
    /// </summary>
    void ICard.CardNum()
    {

    }
}
