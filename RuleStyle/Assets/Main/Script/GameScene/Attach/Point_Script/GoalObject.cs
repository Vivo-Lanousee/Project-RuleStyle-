using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �S�[���|�C���g�ɃA�^�b�`����Ă��邩�ǂ����Ŕ��肷��
/// �i���܂肱����Ŕ��肳����z��ł͂Ȃ��j
/// </summary>
public class GoalObject : MonoBehaviour
{

    /// <summary>
    /// Debug�p
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�S�[���ɓ����Ă��܂�");
    }
}
