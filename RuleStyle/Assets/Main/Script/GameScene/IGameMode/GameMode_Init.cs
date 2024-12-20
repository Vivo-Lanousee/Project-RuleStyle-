using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode_Init :IGameMode
{
    GameSceneManager GameSceneManager;
    public GameMode_Init(GameSceneManager gameSceneManager)
    {
        GameSceneManager = gameSceneManager;
    }

    void IGameMode.Init()
    {
        //�L�[�̃f�[�^�}��
        GameSceneManager.gameManager.Key_playerdata = new Dictionary<int, PlayerData> {
            {1,GameSceneManager.gameManager.playerData_One },
            {2,GameSceneManager.gameManager.playerData_Two },
            {3,GameSceneManager.gameManager.playerData_Three },
            {4,GameSceneManager.gameManager.playerData_Four }
        };

        //�v���C���[�̃f�[�^�̏�����
        //���΂ɐl�����ɍ��̂͂�낵���Ȃ��̂ŁB
        switch (GameSceneManager.gameManager.PlayerNum)
        {
            case 2:
                GameSceneManager.gameManager.Number = new List<int> { 1, 2 };

                GameSceneManager.gameManager.playerData_One = new PlayerData();
                GameSceneManager.gameManager.playerData_Two = new PlayerData();
                break;
            case 3:
                GameSceneManager.gameManager.Number = new List<int> { 1, 2, 3 };

                GameSceneManager.gameManager.playerData_One = new PlayerData();
                GameSceneManager.gameManager.playerData_Two = new PlayerData();
                GameSceneManager.gameManager.playerData_Three = new PlayerData();
                break;
            case 4:
                GameSceneManager.gameManager.Number = new List<int> { 1, 2, 3, 4 };

                GameSceneManager.gameManager.playerData_One = new PlayerData();
                GameSceneManager.gameManager.playerData_Two = new PlayerData();
                GameSceneManager.gameManager.playerData_Three = new PlayerData();
                GameSceneManager.gameManager.playerData_Four = new PlayerData();
                break;
            default:
                Debug.Log("�G���[���o�Ă��܂�(�v���C���[�̐l���ُ̈�)");
                break;
        }

        //���ԃV���b�t��
        GameSceneManager.Shuffle(GameSceneManager.gameManager.Number);
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
