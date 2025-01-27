using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h���
/// </summary>
public interface ICard
{
    public Card_Pattern card_pattern { get; }

    /// <summary>
    /// �J�[�h�̎�����
    /// </summary>
    public PlayerSessionData PlayerData { get; set; }

    /// <summary>
    /// �J�[�h���o������m��
    /// </summary>
    public float? ProbabilityNum { get; }

    /// <summary>
    /// �J�[�h�̕���
    /// </summary>
    string CardName { get;}

    //public List<int> BlueEffect { get; set; }

    /// <summary>
    /// �J�[�h����
    /// </summary>
    void CardNum();

    /// <summary>
    /// �J�[�h�̏����v���C���\�̕ύX
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
    Yellow,
    Green,
    Red
}