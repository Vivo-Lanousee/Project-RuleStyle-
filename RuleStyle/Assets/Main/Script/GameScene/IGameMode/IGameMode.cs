using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���s���s�ׂ�State
/// </summary>
public interface IGameMode
{
    void Init();
    void Update();
    void FixUpdate();
    void Exit();
}
