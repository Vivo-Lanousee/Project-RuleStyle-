using UnityEngine;

public class Player_Attach : MonoBehaviour
{
    public PlayerSessionData _playerData=null;


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
