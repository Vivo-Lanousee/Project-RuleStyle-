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
public class PlayerSessionData 
{
    public PlayerSessionData() {
        Blue.Subscribe(_ => { }).AddTo(_disposed);
    }

    /// <summary>
    /// C
    /// </summary>
    private readonly CompositeDisposable _disposed = new CompositeDisposable();

    /// <summary>
    /// �폜��
    /// </summary>
    ~PlayerSessionData()
    {
        _disposed.Dispose();
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

    ReactiveProperty<ICard> Blue;
    /// <summary>
    /// ���ʑΏۂ̃J�[�h
    /// </summary>
    public ICard card_Blue;
    /// <summary>
    /// ���_�̏���
    /// </summary>
    public ICard card_Orenge;
    /// <summary>
    /// ���_�ŉ��𓾂�̂��ǂ����i�J�[�h�����_���j
    /// </summary>
    public ICard card_Yellow;
    /// <summary>
    /// ���_�̌v�Z���@
    /// </summary>
    public ICard card_Green;
    /// <summary>
    ///�@�J�[�h�̎Q�Ƃ��鐔��ύX����
    /// </summary>
    public ICard card_Red;

    /// <summary>
    /// �v���C���[�̋�(����쐬���ɃA�^�b�`����j
    /// </summary>
    public GameObject Player_GamePiece;

    public void ChangeCard()
    {

    }

    public void ShotPoint()
    {
        //����쐬
        card_Orenge.CardNum();

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