using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �����ȊO
/// </summary>
public class Card_Red_Other_than : ICard, ICard_Red
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => 30;
    Card_Pattern ICard.card_pattern => Card_Pattern.Red;
    
    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�����ȊO��";

    /// <summary>
    /// �J�[�hBlue�̎��݂̂̎����ƂȂ�B
    /// </summary>
    public List<int> EffectMember => new List<int> {};
    Image ICard.cardUI { get; set; }
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
                //
                EffectMember.Add(i.Key);
            }
        }
    }
}
