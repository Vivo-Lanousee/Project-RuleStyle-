using UnityEngine;
using System;
using UniRx.Triggers;
public class Player_Attach : MonoBehaviour
{
    PlayerSessionData _playerData=null;

    /// <summary>
    /// �폜���ꂽ��
    /// </summary>
    private void OnDestroy()
    {
        if(_playerData != null)
        {
            _playerData.Death = true;
        }
    }
}
