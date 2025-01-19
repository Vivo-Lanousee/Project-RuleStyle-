using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ȊO
/// </summary>
public class Card_Blue_Other_than : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;
    
    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�����ȊO��";

    /// <summary>
    /// �͑S�ĕԂ�l�Ō��ʂ��s��
    /// </summary>
    void ICard.CardNum()
    {
        Debug.Log("�J�[�h�����ȊO");
        GameSessionManager gameManager = GameSessionManager.Instance();
        foreach (var i in gameManager.Session_Data)
        {
            if (i.Key!=PlayerData.PlayerId)
            {
                PlayerData.EffectPlayer_Id.Add(i.Key);
            }
        }
    }
}
