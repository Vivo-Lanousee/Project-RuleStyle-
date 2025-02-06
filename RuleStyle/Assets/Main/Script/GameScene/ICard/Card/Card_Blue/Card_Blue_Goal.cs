using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// �S�[���s�����Ƃ��̔���J�[�h
/// </summary>
public class Card_Blue_Goal : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => null;
    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;

    Sprite ICard.cardUI { get; set; }
    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�S�[����";

    void ICard.CardNum()
    {
        if(PlayerData != null)
        {
            //�V���b�g�C�x���g�̔O�̂��߂̏�����
            PlayerData.BlueTrigger?.Dispose();

            //�V���b�g�C�x���g�o�^
            PlayerData.BlueTrigger = PlayerData.Player_GamePiece.OnTriggerEnterAsObservable()
                .Take(1)//���Ŏ��R��Dispose����悤�ɂ���B
                .Subscribe(collider =>
            { 
                if (collider.gameObject.GetComponent<GoalObject>() != null)
                {
                    PlayerData.Success();
                }
            }).AddTo(PlayerData.Player_GamePiece);
        }
    }
}
