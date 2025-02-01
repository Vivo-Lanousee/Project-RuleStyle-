using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

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
                .Take(1)//���Ŏ��R��Dispose����悤�ɂ���B
                .Subscribe(x => 
                {
                    if (x.gameObject.GetComponent<Player_Attach>() != null)
                    {
                        Debug.Log("�A�^�b�N����");
                        PlayerData.Success();
                    }
                }).AddTo(PlayerData.Player_GamePiece);
        }
    }
}
