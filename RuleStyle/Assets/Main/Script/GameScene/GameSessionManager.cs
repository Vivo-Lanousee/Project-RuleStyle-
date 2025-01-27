using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// �Q�[���V�[�����A�A�^�b�`����Ă��鎖��z�肵�Ă��܂��B
/// </summary>
public class GameSessionManager : MonoBehaviour
{
    /// <summary>
    /// �Q�[���}�l�[�W���[�Ƀv���C���[�����������Ă����K�v������̂��H
    /// ������B�Q�[���I���͂�����Ŏ擾�����Ă������Ƃ�
    ///�@�v���C���\�l�[�����̏��̓Q�[���}�l�[�W���[�Őݒ肳���Ă���
    /// </summary>
    public GameManager gameManager;

    #region Static��
    protected static GameSessionManager instance;
    /// <summary>
    /// �Q�[���V�[���}�l�[�W���[�ɐڑ�����ׂ̊֐�
    /// </summary>
    /// <returns></returns>
    public static GameSessionManager Instance()
    {
        return instance;
    }
    #endregion
    /// <summary>
    /// ���݂̑���
    /// </summary>
    public IGameMode gamemode;

    public Dictionary<int, PlayerSessionData> Session_Data = null;

    public GameSceneContext sceneContext = new GameSceneContext();

    #region �v���C���[�̋�̕ϐ��B
    public GameObject PlayerGameObject_One;

    public GameObject PlayerGameObject_Two;

    public GameObject PlayerGameObject_Three;

    public GameObject PlayerGameObject_Four;
    #endregion

    /// <summary>
    /// Main��UI
    /// </summary>
    public Main_UI_Component UI;

    /// <summary>
    /// ����
    /// </summary>
    public List<int> TurnList = new List<int>();
    
    //���݂̃v���C���[�̏���
    public int CurrentTurnNum = 0;

    /// <summary>
    /// ��̐����ꏊ
    /// </summary>
    public Vector3 PieceStartPoint=new Vector3(0,10,0);
    void Start()
    {
        TurnList.Clear();

        //�������ɃA�^�b�`����Ă��邱�Ƃ��z�肳��Ă����
        instance = this;
        //
        gameManager = GameManager.Instance();
        
        //�V�����l�����Q�Ƃ��ĐV�����f�[�^���쐬����
        switch (gameManager.PlayerNum)
        {
            case 2:
                Session_Data = new Dictionary<int, PlayerSessionData>
                {
                    {1,new PlayerSessionData() },
                    {2,new PlayerSessionData() }
                };
                TurnList = new List<int> {
                    1,2
                };
                break;
            case 3:
                Session_Data = new Dictionary<int, PlayerSessionData>
                {
                    {1,new PlayerSessionData() },
                    {2,new PlayerSessionData() },
                    {3,new PlayerSessionData() }
                };
                TurnList = new List<int> {
                    1,2,3
                };
                break;
            case 4:
                Session_Data = new Dictionary<int, PlayerSessionData>
                {
                    {1,new PlayerSessionData() }, 
                    {2,new PlayerSessionData() },
                    {3,new PlayerSessionData() },
                    {4,new PlayerSessionData() }
                };
                TurnList = new List<int> {
                    1,2,3,4
                };
                break;
                }

        TurnList = Shuffle(TurnList);
        //Init����H
        sceneContext.Mode_Change(new GameMode_Init(this));
    }

    //���̃v���C���[�̃f�[�^
    public PlayerSessionData NowPlayer (){
        return Session_Data[TurnList[CurrentTurnNum]];
    }

    /// <summary>
    /// �J�[�h���h���[����B�����͑ΏۂƖ���
    /// </summary>
    public void DeckDraw(PlayerSessionData player,int num)
    {
        //�S�J�[�h
        List<ICard> cards = new List<ICard>
                {
                    new Card_Blue_EffectOne(),
                    new Card_Blue_EffectTwo(),
                    new Card_Blue_EffectThree(),
                    new Card_Blue_EffectFour(),
                    new Card_Blue_Other_than(),
                    new Card_Blue_MySelf(),
                    new Card_Green_Minus(),
                    new Card_Green_Plus(),
                    new Card_Green_Multiplication(),
                    new Card_Orange_Attack(),
                    new Card_Orange_OverField(),
                    new Card_Orange_Goal(),
                    new Card_Red_One(),
                    new Card_Red_Two(),
                    new Card_Red_Three(),
                    new Card_Yellow_CardDraw(),
                    new Card_Yellow_Point()
                };
        //������h���[���\
        for (var i = 0; i < num; i++)
        {
            float Max = 0;
            float Current = 0;

            //�m�����a
            foreach (var x in cards)
            {
                if (x.ProbabilityNum != null)
                {
                    Max += (float)x.ProbabilityNum;
                }
            }

            var random = UnityEngine.Random.Range(0, Max);

            bool DrawSuccess = false;
            foreach (var x in cards)
            {
                if (x.ProbabilityNum != null)
                {
                    Current += (float)x.ProbabilityNum;

                    if (Max<Current)
                    {
                        player.HandCards.Add(x);
                        DrawSuccess = true;
                        break;
                    }
                }
            }
            //���������a�ȏ�̎������v�f��
            if (DrawSuccess == false)
            {
                player.HandCards.Add(cards[cards.Count]);
            }
        }
        //Draw��ʂɈڍs����B
    }

    

    /// <summary>
    /// GameManager����GameSessionManager�Ƀf�[�^���������Ă����B
    /// </summary>
    public void OnLoadSessionData()
    {
        //�v���C���[���ƃv���C���[ID�̑��
        foreach (var i in Session_Data) {
            i.Value.PlayerId=GameManager.Variable_Data[i.Key].Id;
            i.Value.PlayerName = GameManager.Variable_Data[i.Key].PlayerName;
        }
    }

    /// <summary>
    /// ���ԃV���b�t��
    /// </summary>
    public List<int> Shuffle(List<int> array)
    {
        for (var i = array.Count - 1; i > 0; --i)
        {
            // 0�ȏ�i�ȉ��̃����_���Ȑ������擾
            // Random.Range�̍ő�l�͑�Q���������Ȃ̂ŁA+1���邱�Ƃɒ���
            var j = UnityEngine.Random.Range(0, i + 1);

            // i�Ԗڂ�j�Ԗڂ̗v�f����������
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }

        return array;
    }

    private void OnDestroy()
    {
        Debug.Log("�j��");
        foreach (var t in Session_Data)
        {
            t.Value.Dispose();
        }
    }

    private void Update() => sceneContext._currentgameMode?.Update();

    private void FixedUpdate() => sceneContext._currentgameMode?.FixUpdate();
}
