using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

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

    Sprite cardUI { get; set; }

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

    public void Card_LoadData()
    {
        Addressables.LoadAssetAsync<Sprite>(CardName).Completed += _ =>
        {
            if (_.Result == null) 
            {
                return;
            };
            cardUI =_.Result;
        };
    }
    
}

/// <summary>
/// �p�^�[��
/// </summary>
public enum Card_Pattern
{
    Purple,
    Blue,
    Yellow,
    Green,
    Red
}