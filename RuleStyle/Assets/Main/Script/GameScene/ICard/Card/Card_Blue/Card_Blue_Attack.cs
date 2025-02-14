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
        if (PlayerData != null)
        {
            Debug.Log("�A�^�b�N����쐬");
            //�V���b�g�C�x���g�̔O�̂��߂̏�����
            PlayerData.BlueTrigger?.Dispose();

            List<GameObject> EffectObjects = new List<GameObject>();
            //���ʑΏۂ�GameObjectList�쐬
            foreach (var effect in PlayerData.EffectPiecePlayer_Id)
            {
                //�I�u�W�F�N�g�����݂��Ȃ��ꍇ�A����͍s���Ȃ��B
                if(GameSessionManager.Instance().Session_Data[effect].Player_GamePiece != null)
                {
                    EffectObjects.Add(GameSessionManager.Instance().Session_Data[effect].Player_GamePiece);
                }
                
            }

            PlayerData.BlueTrigger=EffectObjects.ConvertAll(obj => obj.GetComponent<Collider>()
            .OnCollisionEnterAsObservable())
                .Merge()
                .Where(collision => collision.gameObject.GetComponent<Player_Attach>()!=null)//�v���C���[�̂�
                .Take(1)
                .Subscribe(_ => 
                {
                    Debug.Log("���萬��");
                    PlayerData.Success_Local();
                });

            /*
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
                        PlayerData.Success_Local();
                    }
                }).AddTo(PlayerData.Player_GamePiece);
            }
            */

            //PlayerData.BlueTrigger = 
               // PlayerData.
        }
    }
}
