using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode_MainMode : IGameMode
{
    GameSessionManager GameSceneManager;
    public GameMode_MainMode(GameSessionManager gameSceneManager)
    {
        GameSceneManager = gameSceneManager;
    }


    /// <summary>
    /// �����Ŏ��̃v���C���[�̏�������
    /// </summary>
    void IGameMode.Init()
    {
        
    }

    /// <summary>
    /// �V���b�g�����쐬����B
    /// </summary>
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
