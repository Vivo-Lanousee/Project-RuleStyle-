using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card_Red_EffectThree : ICard, ICard_Red
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => 50;
    Card_Pattern ICard.card_pattern => Card_Pattern.Red;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "P3��";
    Image ICard.cardUI { get; set; }
    /// <summary>
    /// �J�[�hBlue�̎��݂̂̎����ƂȂ�B
    /// </summary>
    public List<int> EffectMember => new List<int> {3};
    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {
        //PlayerData.EffectPlayer_Id.Add(3);
    }
}
