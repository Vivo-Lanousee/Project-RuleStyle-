using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Blue_EffectOne : ICard
{
    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="_playerSettingData"></param>
    public Card_Blue_EffectOne(PlayerSessionData _playerSettingData)
    {
        PlayerData = _playerSettingData;
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
        Debug.Log("�J�[�h�����ȊO");
    }
}
