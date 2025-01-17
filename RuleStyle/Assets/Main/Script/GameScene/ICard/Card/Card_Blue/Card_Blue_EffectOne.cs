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
    string ICard.CardName => "P1��";

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {
        PlayerData.EffectPlayer_Id.Add(1);
    }
}
