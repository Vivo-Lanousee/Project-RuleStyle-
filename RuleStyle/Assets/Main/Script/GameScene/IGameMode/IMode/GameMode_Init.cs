using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������̌�A�Q�[���̉��o��AMainMode�Ɉڍs����B
/// </summary>

public class GameMode_Init :IGameMode
{
    GameSessionManager GameSceneManager;
    public GameMode_Init(GameSessionManager gameSceneManager)
    {
        GameSceneManager = gameSceneManager;
    }

    void IGameMode.Init()
    {
        
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
