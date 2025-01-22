using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

/// <summary>
/// ��X�e�[�W�O�ɏo���Ƃ��ɔ�������
/// </summary>
public class Card_Orange_OverField : ICard
{

    public PlayerSessionData PlayerData { get; set; } = null;

    Card_Pattern ICard.card_pattern => Card_Pattern.Orange;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "��O��";

    /// <summary>
    /// 
    /// </summary>
    void ICard.CardNum()
    {
        if (PlayerData != null)
        {
            //�V���b�g�C�x���g�̔O�̂��߂̏�����
            PlayerData.OrangeTrigger?.Dispose();

            //�V���b�g�C�x���g�o�^
            PlayerData.OrangeTrigger = PlayerData.Player_GamePiece
                .OnDestroyAsObservable()
                .Subscribe(x => 
                { 
                    Debug.Log("��O����"); 
                    //OnDestroy���ƃS�[���Ƃ��̔���Ƃ�����ɋ��������B�B�B
                }
                )
                .AddTo(PlayerData.Player_GamePiece);
        }
    }
}
