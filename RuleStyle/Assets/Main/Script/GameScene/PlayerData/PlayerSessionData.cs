using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// �Z�b�V�����v���C���[�f�[�^
/// MonoBehavior�͎g�킸�APlayer�̋�ɃA�^�b�`����X�N���v�g�͕ʘg�ō쐬����
/// </summary>
public class PlayerSessionData:IDisposable
{
    /// <summary>
    /// �J�[�h�S�ď�����
    /// </summary>
    public void Init()
    {
        gameSessionManager = GameSessionManager.Instance();

        SubScribe();
        Reset_All();
    }

    /// <summary>
    /// �L�����N�^�[�̃J�[�h�̏������Z�b�g����
    /// </summary>
    public void Reset_All()
    {
        Remove_Blue_EffectAward();
        Remove_Orange();
        Remove_Yellow();
        Remove_Green();
        Remove_Red();
    }
    //Remove-�F�[�[����F�J�[�h����J�[�h�ɏ�����
    #region Remove�֐�

    private void Remove_Blue_EffectPrise()
    {
        Card_Blue_EffectPiece.Value = new Card_Blue_MySelf();
    }
    public void Remove_Blue_EffectAward()
    {
        Card_Blue_EffectAward.Value = new Card_Blue_MySelf();
    }
    public void Remove_Orange()
    {
        Card_Blue_EffectAward.Value= new Card_Orange_Goal();
    }
    public void Remove_Yellow()
    {
        Card_Yellow.Value = new Card_Yellow_Point();
    }
    public void Remove_Green()
    {
        Card_Green.Value = new Card_Green_Plus();  
    }
    public void Remove_Red()
    {
        Card_Red.Value = new Card_Red_One();
    }
    #endregion

    public void Dispose()
    {   
        //������������̎󂯎M�B
        //������Dispose�̓I�u�W�F�N�g���j�󂳂ꂽ�Ƃ��Ă����������Ȃ��悤�ɂ���B
        ShotEvent?.Dispose();

        //ReactivePropety
        Card_Blue_EffectAward?.Dispose();
        Card_Green?.Dispose();
        Card_Yellow?.Dispose();
        Card_Green?.Dispose();
        Card_Red?.Dispose();
    }

    /// <summary>
    /// ��ɃJ�[�h�ύX���̏����ɂ���
    /// </summary>
    public void SubScribe()
    {
        Card_Blue_EffectPiece
            .Subscribe(_ =>
            {
                _.Card_PlayerChange(this);
                _.CardNum();
            });


        Card_Blue_EffectPiece.Subscribe(_ => { 
            EffectPrisePlayer_Id.Clear();
            _.Card_PlayerChange(this);
            _.CardNum();
            //-------------------------------------------
            //IBlueCard�Ɉ�x�L���X�g���ĕϊ�����B
            ICard_Blue blue = (ICard_Blue)_;
            //�����������s���Ȃ񂾂��ǖ��Ȃ��̂��낤��
            EffectPrisePlayer_Id = blue.EffectMember;
            //-----------------------------------------------

            Debug.Log("��(�K�p�Ώ�)�J�[�h�ύX");
        });

        Card_Blue_EffectAward
            .Subscribe(_ => {
                EffectAwardPlayer_Id.Clear();
                _.Card_PlayerChange(this);
                _.CardNum();

                //-------------------------------------------
                //IBlueCard�Ɉ�x�L���X�g���ĕϊ�����B
                ICard_Blue blue=(ICard_Blue)_;
                //�����������s���Ȃ񂾂��ǖ��Ȃ��̂��낤��
                EffectAwardPlayer_Id = blue.EffectMember;
                //-----------------------------------------------

                Debug.Log("��(��V�Ώ�)�J�[�h�ύX");
            });

        //����J�[�h�ύX�Ȃ̂�CardNum�͍s��Ȃ��BUI�ύX�̂�
        Card_Orange.Subscribe(_ => 
        {
            _.Card_PlayerChange(this);
            Debug.Log("�I�����W(���[���E����)�J�[�h�ύX");
        });
        //�v�Z���@�Ȃ̂�CardNum�͍s��Ȃ��BUI�ύX�̂�
        Card_Green.Subscribe(_ =>
        {
            _.Card_PlayerChange(this);
            Debug.Log("��(�v�Z���@�̕ύX)�J�[�h�ύX");
        });
        //CardNum�͍s��Ȃ��BUI�ύX�̂�
        Card_Yellow.Subscribe(_ => 
        {
            _.Card_PlayerChange(this);
            Debug.Log("���i���_�j�J�[�h�ύX");
        });
        Card_Red.Subscribe(_ =>
        {
            _.Card_PlayerChange(this);
            _.CardNum();
            Debug.Log("�ԁi���l�j�J�[�h�ύX");
        });
    }

    /// <summary>
    /// �J�[�h��N���ɕύX���鎞�̏����B�u���ꂶ��_���������B�u���[���������Ȃ�v
    /// </summary>
    public void GiveCard(ICard card,PlayerSessionData player)
    {
        switch (card.card_pattern)
        {
            case Card_Pattern.Blue:
                //player.Card_Blue_EffectAward.Value = card;
                BlueGiveCard();
                break;
            case Card_Pattern.Orange: 
                player.Card_Orange.Value = card;
                break;
            case Card_Pattern.Yellow: 
                 player.Card_Yellow.Value = card;
                break;
            case Card_Pattern.Green:
                player.Card_Green.Value = card;
                break;
            case Card_Pattern.Red:
                player.Card_Red.Value = card;
                break;
        }
    }
    /// <summary>
    /// �̃J�[�h��N���ɕt�^���鎞�̓���֐��B�iUI�Ɋւ���Ă��܂��B�j
    /// </summary>
    public void BlueGiveCard()
    {

    }


    /// <summary>
    /// �}�l�[�W���[�iUI�ύX���̎��̏���
    /// </summary>
    private GameSessionManager gameSessionManager=null;


    /// <summary>
    /// ���[���S��
    /// </summary>
    public string Rule { get; }
    
    public int PlayerId;

    public string PlayerName=null;

    /// <summary>
    /// �V���b�g���̔���C�x���g
    /// </summary>
    public IDisposable ShotEvent = null;
    /// <summary>
    /// Orange�̃J�[�h������������V���b�g������C�x���g
    /// </summary>
    public IDisposable OrangeTrigger = null;

    /// <summary>
    /// ���ʓK�p�Ώ�
    /// </summary>
    public List<int> EffectPrisePlayer_Id = new List<int>();

    /// <summary>
    /// ��V�Ώ� 
    /// </summary>
    public List<int> EffectAwardPlayer_Id = new List<int>();

    /// <summary>
    /// ��D�̃J�[�h���X�g
    /// </summary>
    public List<ICard> HandCards = new List<ICard>();

    //�l���[���������̐����B�i�ԃJ�[�h�ŕύX�����j
    public int RuleSuccessNum = 0;

    /// <summary>
    /// �v���C���[�̓_��
    /// </summary>
    public int PlayerPoint=0;

    #region �J�[�h�̕ϐ�

    /// <summary>
    /// �ǂ̋�Ɍ��ʂ��K������邩�ǂ����B
    /// </summary>
    public ReactiveProperty<ICard> Card_Blue_EffectPiece = new ReactiveProperty<ICard>();

    /// <summary>
    /// ��V���ʑΏۂ̃J�[�h
    /// </summary>
    public ReactiveProperty<ICard> Card_Blue_EffectAward = new ReactiveProperty<ICard>();

    /// <summary>
    /// ���_�̏���(�����͕ύX���ł͂Ȃ��̂Ō��ʂ�
    /// Reactive�Ŕ���������̂ł͖����B
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

    #endregion


    /// <summary>
    /// �v���C���[�̋�(����쐬���ɃA�^�b�`����j
    /// </summary>
    public GameObject Player_GamePiece;
    //��O����
    public bool Death=false;

    /// <summary>
    /// �l���[�����萬�������True
    /// </summary>
    public bool SuccessPoint=false;

    /// <summary>
    /// �l���[���������̊֐�
    /// </summary>
    public void Success()
    {
        SuccessPoint = true;
    }

    /// <summary>
    /// �V���b�g���ɔ������鎞�B
    /// </summary>
    public void ShotPoint()
    {
        //����쐬
        Card_Orange.Value.CardNum();

        //�I����������s��
        Player_GamePiece.transform.ObserveEveryValueChanged(x => x.position)
            .Throttle(TimeSpan.FromSeconds(1))
            .Take(1)//���Ŏ��R��Dispose����悤�ɂ���B
            .Subscribe(x =>
            { 
                Debug.Log("�V���b�g�I��");

                if (SuccessPoint)
                {
                    RuleSucces();
                    SuccessPoint = false;
                }
            }) .AddTo(Player_GamePiece);
    }


    /// <summary>
    /// �^�[���I�����̂��ꂱ��B
    /// </summary>
    public void TurnEnd()
    {

    }

    /// <summary>
    /// �l���[���������̃|�C���g�B
    /// </summary>
    public void RuleSucces()
    {
        //�l���[���B�����̃����[�h
        Card_Yellow.Value.CardNum();
    }
    /// <summary>
    /// �S�[���������̃����[�h
    /// </summary>
    public void GoalReward()
    {

    }
    
    /// <summary>
    /// ��Ֆʏ�ɑ��݂��Ȃ��ꍇ�̃X�N���v�g
    /// </summary>
    /// <param name="MyPiece"></param>
    public void PlayerPieceCreate()
    {
        switch (PlayerId) 
        {
        case 1:
                Player_GamePiece=UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_One,gameSessionManager.PieceStartPoint,Quaternion.identity);
                break;
        case 2:
                Player_GamePiece = UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_Two, gameSessionManager.PieceStartPoint, Quaternion.identity);
                break;
        case 3:
                Player_GamePiece = UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_Three, gameSessionManager.PieceStartPoint, Quaternion.identity);
                break;
        case 4:
                Player_GamePiece = UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_Four, gameSessionManager.PieceStartPoint, Quaternion.identity);
                break;
        }
        Death = false;
    }
}