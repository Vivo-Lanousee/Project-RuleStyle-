using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card_Red_EffectTwo : ICard, ICard_Red
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => 50;
    Card_Pattern ICard.card_pattern => Card_Pattern.Red;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "P2��";

    /// <summary>
    /// �J�[�h�Ԃ̎��݂̂̎����ƂȂ�B
    /// </summary>
    public List<int> EffectMember => new List<int> { 2 };
    Sprite ICard.cardUI { get; set; }
    void ICard.CardNum()
    {
        //PlayerData.EffectPlayer_Id.Add(2);
    }
}
