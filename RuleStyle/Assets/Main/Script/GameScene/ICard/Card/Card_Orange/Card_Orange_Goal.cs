using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �S�[���s�����Ƃ��̔���J�[�h
/// </summary>
public class Card_Orange_Goal : ICard
{
    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="_playerSettingData"></param>
    public Card_Orange_Goal(PlayerSessionData _playerSettingData)
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
