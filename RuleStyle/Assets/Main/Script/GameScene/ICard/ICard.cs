using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h���
/// </summary>
public interface ICard
{
    public Card_Pattern card_pattern { get; }

    public PlayerSessionData PlayerData { get; set; }

    

    /// <summary>
    /// �J�[�h�̕���
    /// </summary>
    string CardName { get;}

    /// <summary>
    /// �J�[�h����
    /// </summary>
    void CardNum();

    /// <summary>
    /// 
    /// </summary>
    public void Card_PlayerChange(PlayerSessionData player)
    {
        PlayerData = player;
    }
}

/// <summary>
/// �p�^�[��
/// </summary>
public enum Card_Pattern
{
    Blue,
    Orange,
    Green,
    Red
}