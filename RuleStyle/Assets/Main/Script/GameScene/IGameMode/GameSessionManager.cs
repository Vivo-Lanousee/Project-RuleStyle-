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

    
    /// <summary>
    /// ����
    /// </summary>
    public List<int> TurnList = new List<int>();

    void Start()
    {
        TurnList.Clear();

        //�������ɃA�^�b�`����Ă��邱�Ƃ��z�肳��Ă����
        instance = this;

        gameManager = GameManager.Instance();


        gameManager.PlayerNum = 4;
        sceneContext.Mode_Change(new GameMode_Init(this));


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
                return;
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
                return;
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
                return;
        }

        Shuffle(TurnList);
    }

    public void OnLoadSessionData()
    {
        //gameManager.
        Debug.Log(GameManager.Variable_Data);
    }
    private void Update() => sceneContext._currentgameMode?.Update();

    private void FixedUpdate() => sceneContext._currentgameMode?.FixUpdate();

    /// <summary>
    /// ���ԃV���b�t��
    /// </summary>
    public void Shuffle(List<int> array)
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
    }
}
