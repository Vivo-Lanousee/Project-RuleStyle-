using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���σJ�[�h���Q�Ƃ���
/// </summary>
public class Card_Yellow_CardDraw : ICard
{
    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="_playerSettingData"></param>
    public Card_Yellow_CardDraw(PlayerSessionData _playerSettingData)
    {
        PlayerData = _playerSettingData;
    }

    PlayerSessionData PlayerData;

    Card_Pattern ICard.card_pattern => Card_Pattern.Orange;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�S�[����";

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {

    }
}
