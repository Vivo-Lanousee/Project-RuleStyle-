using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
/// <summary>
/// ��X�e�[�W�O�ɏo���Ƃ��ɔ�������
/// </summary>
public class Card_Blue_OverField : ICard
{

    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => 45;

    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;
    
    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "��O��";

    Sprite ICard.cardUI { get; set; }

    void ICard.CardNum()
    {
        if (PlayerData != null)
        {
            //�V���b�g�C�x���g�̔O�̂��߂̏�����
            PlayerData.BlueTrigger?.Dispose();

            //�V���b�g�C�x���g�o�^
            PlayerData.BlueTrigger=PlayerData.Player_GamePiece.OnTriggerEnterAsObservable()
                .Take(1)//���Ŏ��R��Dispose����悤�ɂ���B
                .Subscribe(collider =>
                {
                    if (collider.gameObject.GetComponent<OutPosition>() != null)
                    {
                        Debug.Log("��O�ł�");
                        PlayerData.Success();
                    }
                }).AddTo(PlayerData.Player_GamePiece);
        }

    }
}
