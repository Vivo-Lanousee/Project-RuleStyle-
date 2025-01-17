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
        gameSessionManager = GameSessionManager.Instance();

        Card_Blue = new ReactiveProperty<ICard>(new Card_Blue_MySelf(this));

        SubScribe();


    }
    
    public void Dispose()
    {   
        //������������̎󂯎M�B//������Dispose�̓I�u�W�F�N�g���j�󂳂ꂽ�Ƃ��Ă����������Ȃ��悤�ɂ���B
        ShotEvent?.Dispose();
        PointEvent?.Dispose();

        //ReactivePropety
        Card_Blue?.Dispose();
        Card_Green?.Dispose();
        Card_Yellow?.Dispose();
        Card_Green?.Dispose();
        Card_Red?.Dispose();
    }

    public void SubScribe()
    {
        Debug.Log("�T�u�X�N���C�u");
        //�ΏۂȂ̂ōs���B
        Card_Blue
            .Subscribe(_ => {
                EffectPlayer_Id.Clear();
                _.CardNum();
                Debug.Log("�ύX����܂���");
            });

        Card_Orange.Subscribe(_ => 
        { 
        
        });
        //�v�Z���@�Ȃ̂�CardNum�͍s��Ȃ��B
        Card_Green.Subscribe(_ =>
        {
            
        });

    }



    /// <summary>
    /// �}�l�[�W���[�iUI�ύX���̎��̏���
    /// </summary>
    private GameSessionManager gameSessionManager=null;

    /// <summary>
    /// ���[���S��
    /// </summary>
    public string Rule { get; }
    /// <summary>
    /// ID
    /// </summary>
    public int PlayerId;

    /// <summary>
    /// �v���C���[�l�[��
    /// </summary>
    public string PlayerName=null;
    

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
    public ReactiveProperty<ICard> Card_Blue = new ReactiveProperty<ICard>();

    /// <summary>
    /// ���_�̏���(�����͕ύX���ł͂Ȃ��̂Ō��ʂ�Reactive�Ŕ���������̂ł͖����B
    /// </summary>
    public ReactiveProperty<ICard> Card_Orange=new ReactiveProperty<ICard>();

    /// <summary>
    /// ���_�ŉ��𓾂�̂��ǂ����i�J�[�h�����_���j
    /// </summary>
    public ReactiveProperty<ICard> Card_Yellow=new ReactiveProperty<ICard>();

    /// <summary>
    /// ���_�̌v�Z���@
    /// </summary>
    public ReactiveProperty<ICard> Card_Green = new ReactiveProperty<ICard>();

    /// <summary>
    ///�@�J�[�h�̎Q�Ƃ��鐔��ύX����
    /// </summary>
    public ReactiveProperty<ICard> Card_Red = new ReactiveProperty<ICard>();

    /// <summary>
    /// �v���C���[�̋�(����쐬���ɃA�^�b�`����j
    /// </summary>
    private GameObject Player_GamePiece;

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

    /// <summary>
    /// 
    /// </summary>
    public void PlayerPieceCreate(GameObject MyPiece)
    {

    }
}