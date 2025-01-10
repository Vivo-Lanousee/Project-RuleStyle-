using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// �Z�b�V�����v���C���[�f�[�^
/// MonoBrhavior�͎g�킸�APlayer�̋�ɃA�^�b�`����X�N���v�g�͕ʘg�ō쐬����
/// </summary>
[Serializable]
public class PlayerSessionData:IDisposable
{
    public PlayerSessionData() 
    {
        //Blue.Subscribe(_ => { });
    }
    
    public void Dispose()
    {
        ShotEvent?.Dispose();
        PointEvent?.Dispose();

        
    }

    /// <summary>
    /// ���[���S��
    /// </summary>
    public string Rule { get; }

    /// <summary>
    /// ID
    /// </summary>
    public int PlayerId;

    /// <summary>
    /// �V���b�g���̔���C�x���g
    /// </summary>
    public IDisposable ShotEvent = null;
    /// <summary>
    /// ���_�̃C�x���g
    /// </summary>
    public IDisposable PointEvent = null;

    /// <summary>
    /// �N�Ɍ��ʂ𔭐������邩�̎Q�ƃ��X�g
    /// �v���C���[�ԍ��ŊǗ�����
    /// </summary>
    public List<int> EffectPlayer_Id=new List<int>();
    
    /// <summary>
    /// ���ʑΏۂ̃J�[�h
    /// </summary>
    public ReactiveProperty<ICard> Card_Blue;

    /// <summary>
    /// ���_�̏���
    /// </summary>
    public ReactiveProperty<ICard> Card_Orange;

    /// <summary>
    /// ���_�ŉ��𓾂�̂��ǂ����i�J�[�h�����_���j
    /// </summary>
    public ReactiveProperty<ICard> Card_Yellow;

    /// <summary>
    /// ���_�̌v�Z���@
    /// </summary>
    public ReactiveProperty<ICard> Card_Green;

    /// <summary>
    ///�@�J�[�h�̎Q�Ƃ��鐔��ύX����
    /// </summary>
    public ReactiveProperty<ICard> Card_Red;

    /// <summary>
    /// �v���C���[�̋�(����쐬���ɃA�^�b�`����j
    /// </summary>
    public GameObject Player_GamePiece;


    public void ShotPoint()
    {
        //����쐬
        Card_Orange.Value.CardNum();

        //�I����������s��
        Player_GamePiece.transform.ObserveEveryValueChanged(x => x.position)
            .Throttle(TimeSpan.FromSeconds(1))
            .Subscribe(x =>
            { 
                Debug.Log("�V���b�g�I��");
                
                PointEvent.Dispose();
            }) .AddTo(Player_GamePiece);
    }
    
    public void Point()
    {

    }

    
}