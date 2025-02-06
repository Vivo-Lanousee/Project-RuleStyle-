using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��m���Ԃ��������ɔ�������
/// </summary>
public class Card_Blue_Attack : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => 35;

    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�����̋�ɓ����邱�Ƃ�";
    Sprite ICard.cardUI { get; set; }
    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {
        Debug.Log("cccccccccccc");
        if (PlayerData != null)
        {
            //�V���b�g�C�x���g�̔O�̂��߂̏�����
            PlayerData.BlueTrigger?.Dispose();

            //�V���b�g�C�x���g�o�^
            if (PlayerData.Player_GamePiece != null) 
            { 
                PlayerData.BlueTrigger = PlayerData?.Player_GamePiece
                .OnTriggerEnterAsObservable()
                .Take(1)//���Ŏ��R��Dispose����悤�ɂ���B
                .Subscribe(x =>
                {
                    if (x.gameObject != null)
                    {
                        Debug.Log("�A�^�b�N����");
                        PlayerData.Success();
                    }
                }).AddTo(PlayerData.Player_GamePiece);
            }
        }
    }
}
