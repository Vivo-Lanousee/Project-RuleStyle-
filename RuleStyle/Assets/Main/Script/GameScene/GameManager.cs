using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �Q�[���}�l�[�W���[
/// </summary>
public class GameManager : SingletonMonoBehaviourBase<GameManager>
{
    [Header("�v���C���[�̐l��")]
    public int PlayerNum = 4;

    /// <summary>
    /// �i���f�[�^
    /// </summary>
    public static Dictionary<int, variable_playerdata> Variable_Data { get; private set; }

    public List<int> Number = null;

    /// <summary>
    /// �N���A����ׂ̓_��
    /// </summary>
    public int ClearPoint = 0;

    /// <summary>
    /// ������
    /// </summary>
    [RuntimeInitializeOnLoadMethod()]
    public static void Init()
    {
        Instance().VariableDataInit();
    }

    /// <summary>
    /// �v���C���[�̃f�[�^��������
    /// </summary>
    public void VariableDataInit()
    {
        Variable_Data = new Dictionary<int, variable_playerdata>()
        {
            { 1, new variable_playerdata(1,"Player1") }
            ,
            { 2, new variable_playerdata(2,"Player2") }
            , 
            { 3, new variable_playerdata(3,"Player3") }
            ,
            { 4, new variable_playerdata(4,"Player4") }
        };
    }


}

public enum GameMode
{
    PlayerOnly
}