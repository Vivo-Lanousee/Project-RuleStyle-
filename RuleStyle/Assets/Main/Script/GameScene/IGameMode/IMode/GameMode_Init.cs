using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���������̌�A�Q�[���̉��o��AMainMode�Ɉڍs����B
/// </summary>

public class GameMode_Init :IGameMode
{
    private GameSessionManager gameSessionManager;
    public GameMode_Init(GameSessionManager gameSceneManager)
    {
        gameSessionManager = gameSceneManager;
    }

    void IGameMode.Init()
    {
        gameSessionManager.TurnList.Clear();
        gameSessionManager.Session_Data.Clear();

        gameSessionManager.cards.Clear();
        gameSessionManager.card_Access.Clear();

        //�V�����l�����Q�Ƃ��ĐV�����f�[�^���쐬����
        switch (gameSessionManager.gameManager.PlayerNum)
        {
            case 2:
                gameSessionManager.Session_Data = new Dictionary<int, PlayerSessionData>
                {
                    {1,new PlayerSessionData() },
                    {2,new PlayerSessionData() }
                };
                gameSessionManager.TurnList = new List<int> {
                    1,2
                };
                gameSessionManager.cards = new List<ICard>
                {
                    new Card_Red_EffectOne(),
                    new Card_Red_EffectTwo(),
                    new Card_Red_Other_than(),
                    new Card_Red_MySelf(),
                    new Card_Green_Minus(),
                    new Card_Green_Plus(),
                    new Card_Blue_Attack(),
                    new Card_Blue_OverField(),
                    new Card_Blue_Goal(),
                    new Card_Purple_One(),
                    new Card_Purple_Two(),
                    new Card_Purple_Three(),
                    new Card_Yellow_CardDraw(),
                    new Card_Yellow_Point()
                };
                break;
            case 3:
                gameSessionManager.Session_Data = new Dictionary<int, PlayerSessionData>
                {
                    {1,new PlayerSessionData() },
                    {2,new PlayerSessionData() },
                    {3,new PlayerSessionData() }
                };
                gameSessionManager.TurnList = new List<int> {
                    1,2,3
                };

                gameSessionManager.cards = new List<ICard>
                {
                    new Card_Red_EffectOne(),
                    new Card_Red_EffectTwo(),
                    new Card_Red_EffectThree(),
                    new Card_Red_Other_than(),
                    new Card_Red_MySelf(),
                    new Card_Green_Minus(),
                    new Card_Green_Plus(),
                    new Card_Blue_Attack(),
                    new Card_Blue_OverField(),
                    new Card_Blue_Goal(),
                    new Card_Purple_One(),
                    new Card_Purple_Two(),
                    new Card_Purple_Three(),
                    new Card_Yellow_CardDraw(),
                    new Card_Yellow_Point()
                };
                break;
            case 4:
                gameSessionManager.Session_Data = new Dictionary<int, PlayerSessionData>
                {
                    {1,new PlayerSessionData() },
                    {2,new PlayerSessionData() },
                    {3,new PlayerSessionData() },
                    {4,new PlayerSessionData() }
                };
                gameSessionManager.TurnList = new List<int> {
                    1,2,3,4
                };

                gameSessionManager.OnLoadSessionData();

                gameSessionManager.cards = new List<ICard>
                {
                    new Card_Red_EffectOne(),
                    new Card_Red_EffectTwo(),
                    new Card_Red_EffectThree(),
                    new Card_Red_EffectFour(),
                    new Card_Red_Other_than(),
                    new Card_Red_MySelf(),
                    new Card_Green_Minus(),
                    new Card_Green_Plus(),
                    new Card_Blue_Attack(),
                    new Card_Blue_OverField(),
                    new Card_Blue_Goal(),
                    new Card_Purple_One(),
                    new Card_Purple_Two(),
                    new Card_Purple_Three(),
                    new Card_Yellow_CardDraw(),
                    new Card_Yellow_Point()
                };
                break;
        }

        //�J�[�h�ɍ����݂�UI�f�[�^�����[�h����
        foreach (var card in gameSessionManager.cards)
        {
            card.Card_LoadData();
        }
        //�J�[�h�̓���ꏊ�ɃX�v���C�g�������[�h�����f�[�^�����Ă����B
        foreach(var card in gameSessionManager.cards)
        {
            gameSessionManager.card_Access.Add(card.CardName,card);
        }

        gameSessionManager.TurnList = gameSessionManager.Shuffle(gameSessionManager.TurnList);

        foreach  (var x in gameSessionManager.Session_Data)
            {
                x.Value.Init();
            }

        gameSessionManager.sceneContext.Mode_Change(new GameMode_MainMode(gameSessionManager));
    }

    void IGameMode.Update()
    {
    }
    

    void IGameMode.FixUpdate()
    {
    }

    void IGameMode.Exit()
    {

    }
}
