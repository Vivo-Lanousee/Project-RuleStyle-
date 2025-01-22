using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

/// <summary>
/// �S�[���s�����Ƃ��̔���J�[�h
/// </summary>
public class Card_Orange_Goal : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

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
        if(PlayerData != null)
        {
            PlayerData.Player_GamePiece.OnTriggerEnterAsObservable()
                .Subscribe(collider =>
            { 
                if (collider.gameObject.GetComponent<GoalObject>() != null)
                {
                    Debug.Log("�S�[��");
                }
            }).AddTo(PlayerData.Player_GamePiece);
        }
    }

}
