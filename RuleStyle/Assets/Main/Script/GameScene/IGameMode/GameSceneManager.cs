using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���V�[�����A�A�^�b�`����Ă��鎖��z�肵�Ă��܂��B
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    public GameManager gameManager;

    /// <summary>
    /// ���݂̑���
    /// </summary>
    public IGameMode gamemode;

    public GameSceneContext sceneContext = new GameSceneContext();
    void Start()
    {
        gameManager = GameManager.Instance();
        gameManager.PlayerNum = 4;
        sceneContext.Mode_Change(new GameMode_Init(this));
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
            var j = Random.Range(0, i + 1);

            // i�Ԗڂ�j�Ԗڂ̗v�f����������
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }
}
