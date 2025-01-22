using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

/// <summary>
/// ��m���Ԃ��������ɔ�������
/// </summary>
public class Card_Orange_Attack : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    Card_Pattern ICard.card_pattern => Card_Pattern.Orange;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�����̋�ɓ����邱�Ƃ�";

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {
        if(PlayerData != null){
            //�V���b�g�C�x���g�̔O�̂��߂̏�����
            PlayerData.OrangeTrigger?.Dispose();

            //�V���b�g�C�x���g�o�^
            PlayerData.OrangeTrigger=PlayerData.Player_GamePiece
                .OnTriggerEnterAsObservable()
                .Subscribe(x => 
                {
                    Debug.Log("�A�^�b�N����");
                }).AddTo(PlayerData.Player_GamePiece);
        }
    }
}
