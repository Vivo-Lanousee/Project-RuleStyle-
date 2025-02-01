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

    #region �J�[�h�̕ϐ�
    /// <summary>
    /// �ǂ̋�Ɍ��ʂ��K������邩�ǂ����B
    /// </summary>
    public ReactiveProperty<ICard> Card_Red_EffectPiece = new ReactiveProperty<ICard>();

    /// <summary>
    /// ��V���ʑΏۂ̃J�[�h
    /// </summary>
    public ReactiveProperty<ICard> Card_Red_EffectAward = new ReactiveProperty<ICard>();

    /// <summary>
    /// ���_�̏���(�����͕ύX���ł͂Ȃ��̂Ō��ʂ�
    /// Reactive�Ŕ���������̂ł͖����B
    /// </summary>
    public ReactiveProperty<ICard> Card_Blue = new ReactiveProperty<ICard>();

    /// <summary>
    /// ���_�ŉ��𓾂�̂��ǂ����i�J�[�h�����_���j
    /// </summary>
    public ReactiveProperty<ICard> Card_Yellow = new ReactiveProperty<ICard>();

    /// <summary>
    /// ���_�̌v�Z���@
    /// </summary>
    public ReactiveProperty<ICard> Card_Green = new ReactiveProperty<ICard>();

    /// <summary>
    ///�@�J�[�h�̎Q�Ƃ��鐔��ύX����
    /// </summary>
    public ReactiveProperty<ICard> Card_Purple = new ReactiveProperty<ICard>();
    #endregion


    /// <summary>
    /// �L�����N�^�[�̃J�[�h�̏������Z�b�g����
    /// </summary>
    public void Reset_All()
    {
        Remove_Red_EffectPiece();
        Remove_Red_EffectAward();
        Remove_Blue();
        Remove_Yellow();
        Remove_Green();
        Remove_Purple();
    }
    //Remove-�F�[�[����F�J�[�h����J�[�h�ɏ�����
    #region Remove�֐�

    private void Remove_Red_EffectPiece()
    {
        Card_Red_EffectPiece.Value = new Card_Red_MySelf();
    }
    public void Remove_Red_EffectAward()
    {
        Card_Red_EffectAward.Value = new Card_Red_MySelf();
    }
    public void Remove_Blue()
    {
        Card_Blue.Value= new Card_Blue_Goal();
    }
    public void Remove_Yellow()
    {
        Card_Yellow.Value = new Card_Yellow_Point();
    }
    public void Remove_Green()
    {
        Card_Green.Value = new Card_Green_Plus();  
    }
    public void Remove_Purple()
    {
        Card_Purple.Value = new Card_Purple_One();
    }
    #endregion

    public void Dispose()
    {   
        //������������̎󂯎M�B
        //������Dispose�̓I�u�W�F�N�g���j�󂳂ꂽ�Ƃ��Ă����������Ȃ��悤�ɂ���B
        ShotEvent?.Dispose();

        //ReactivePropety
        Card_Red_EffectPiece?.Dispose();
        Card_Red_EffectAward?.Dispose();
        Card_Green?.Dispose();
        Card_Yellow?.Dispose();
        Card_Green?.Dispose();
        Card_Purple?.Dispose();
    }

    /// <summary>
    /// ��ɃJ�[�h�ύX���̏����ɂ���
    /// </summary>
    public void SubScribe()
    {
       
        Card_Red_EffectPiece.Subscribe(_ => {
            EffectPiecePlayer_Id.Clear();
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                _.CardNum();

                //-------------------------------------------
                //IBlueCard�Ɉ�x�L���X�g���ĕϊ�����B
                ICard_Red Red = (ICard_Red)_;
                //�����������s���Ȃ񂾂��ǖ��Ȃ��̂��낤��
                EffectPiecePlayer_Id = Red.EffectMember;
                //-----------------------------------------------
            }
            Debug.Log("��(�K�p�Ώ�)�J�[�h�ύX");
        });

        
        Card_Red_EffectAward
            .Subscribe(_ => {
                if (_ != null)
                {
                    EffectAwardPlayer_Id.Clear();
                    _.Card_PlayerChange(this);
                    _.CardNum();

                    //-------------------------------------------
                    //IBlueCard�Ɉ�x�L���X�g���ĕϊ�����B
                    ICard_Red red = (ICard_Red)_;
                    //�����������s���Ȃ񂾂��ǖ��Ȃ��̂��낤��
                    EffectAwardPlayer_Id = red.EffectMember;
                    //-----------------------------------------------

                    Debug.Log("(��V�Ώ�)�J�[�h�ύX");
                }
            });
        
        //����J�[�h�ύX�Ȃ̂�CardNum�͍s��Ȃ��BUI�ύX�̂�
        Card_Blue.Subscribe(_ => 
        {
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                Debug.Log("(���[���E����)�J�[�h�ύX");
            }
        });
        //�v�Z���@�Ȃ̂�CardNum�͍s��Ȃ��BUI�ύX�̂�
        Card_Green.Subscribe(_ =>
        {
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                Debug.Log("(�v�Z���@�̕ύX)�J�[�h�ύX");
            }
        });
        //CardNum�͍s��Ȃ��BUI�ύX�̂�
        Card_Yellow.Subscribe(_ => 
        {
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                Debug.Log("�i���_�j�J�[�h�ύX");
            }
        });
        Card_Purple.Subscribe(_ =>
        {
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                _.CardNum();
                Debug.Log("���l�j�J�[�h�ύX");
            }
        });

    }

    /// <summary>
    /// �J�[�h��N���ɕύX���鎞�̏����B�u���ꂶ��_���������B�u���[���������Ȃ�v
    /// </summary>
    public void GiveCard(ICard card,PlayerSessionData player)
    {
        switch (card.card_pattern)
        {
            case Card_Pattern.Red:
                //player.Card_Blue_EffectAward.Value = card;
                BlueGiveCard();
                break;
            case Card_Pattern.Blue: 
                player.Card_Blue.Value = card;
                break;
            case Card_Pattern.Yellow: 
                 player.Card_Yellow.Value = card;
                break;
            case Card_Pattern.Green:
                player.Card_Green.Value = card;
                break;
            case Card_Pattern.Purple:
                player.Card_Purple.Value = card;
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
    public GameSessionManager gameSessionManager=null;

    /// <summary>
    /// ���[���̃e�L�X�g�ύX
    /// </summary>
    public void RuleText_Exchange()
    {
        string text = Card_Red_EffectPiece.Value.CardName
            + Card_Blue.Value.CardName
            + Card_Red_EffectAward.Value.CardName
            + Card_Yellow.Value.CardName
            + Card_Green.Value.CardName
            + Card_Purple.Value.CardName;

        //�e�L�X�g�ύX
        Debug.Log(text);
    }

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
    /// �̃J�[�h������������V���b�g������C�x���g
    /// </summary>
    public IDisposable BlueTrigger = null;

    /// <summary>
    /// ���ʓK�p�Ώ�
    /// </summary>
    public List<int> EffectPiecePlayer_Id = new List<int>();

    /// <summary>
    /// ��V�Ώ� 
    /// </summary>
    public List<int> EffectAwardPlayer_Id = new List<int>();

    /// <summary>
    /// ��D�̃J�[�h���X�g
    /// </summary>
    public List<ICard> HandCards = new List<ICard>();

    //�l���[���������̕�V�ʁi�ԃJ�[�h�ŕύX�����j
    public int RuleSuccessNum = 0;

    /// <summary>
    /// �v���C���[�̓_��
    /// </summary>
    public int PlayerPoint=0;

    


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
        foreach (var x in gameSessionManager.Session_Data)
        {
            x.Value.Card_Blue.Value.CardNum();
            
        }

        //�I����������s��(�����Ȃ���΋N������
        Player_GamePiece.transform.ObserveEveryValueChanged(x => x.position)
            .Throttle(TimeSpan.FromSeconds(1))
            .Take(1)//���Ŏ��R��Dispose����悤�ɂ���B
            .Subscribe(x =>
            { 
                Debug.Log("�V���b�g�I��");

                //�S���I�����̔�����m�F�A���̌ニ�[���������Ƀ��[���K�p����
                foreach(var y in gameSessionManager.Session_Data) {
                    if (y.Value.SuccessPoint)
                    {
                        y.Value.RuleSucces();
                        y.Value.SuccessPoint = false;
                    }
                }
                RuleText_Exchange();

                TurnEnd();
            })
            .AddTo(Player_GamePiece);
    }


    /// <summary>
    /// �^�[���I�����̂��ꂱ��B
    /// </summary>
    public void TurnEnd()
    {
        
    }

    /// <summary>
    /// �l���[���������̃����[�h�B
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
        gameSessionManager.DeckDraw(this, 2);

        //���σ��[�h�Ɉڍs����B
    }
    
    /// <summary>
    /// ��Ֆʏ�ɑ��݂��Ȃ��ꍇ�̃X�N���v�g
    /// </summary>
    /// <param name="MyPiece"></param>
    public void PlayerPieceCreate()
    {
        gameSessionManager = GameSessionManager.Instance();

        Debug.Log(PlayerId.ToString());
        Debug.Log(gameSessionManager.PlayerGameObject_One);
        switch (PlayerId) 
        {
        case 1:
                Debug.Log("a");
                Player_GamePiece =UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_One,gameSessionManager.PieceStartPoint,Quaternion.identity);
                break;
        case 2:
                Debug.Log("a");
                Player_GamePiece = UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_Two, gameSessionManager.PieceStartPoint, Quaternion.identity);
                break;
        case 3:
                Debug.Log("a");
                Player_GamePiece = UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_Three, gameSessionManager.PieceStartPoint, Quaternion.identity);
                break;
        case 4:
                Debug.Log("a");
                Player_GamePiece = UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_Four, gameSessionManager.PieceStartPoint, Quaternion.identity);
                break;
        }
        Death = false;

        /*
        //�������A�S�̃��[����K�������ꍇ�B
        Player_GamePiece.transform.UpdateAsObservable()
            .Subscribe(x =>
            {

            }).AddTo(Player_GamePiece);*/
    }
}