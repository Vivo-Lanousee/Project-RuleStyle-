using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System.Linq;
using System;

/// <summary>
/// �S�[���p��
/// </summary>
public class Goal : MonoBehaviour
{
    IDisposable dispose;

    void Card()
    {
        dispose=this.OnTriggerEnterAsObservable()
            .Subscribe(collider => {
                //
                if (collider.gameObject.GetComponent<GoalObject>()!=null)
                {
                    Debug.Log("�S�[���I�I�I");
                }
            }).AddTo(this);
    }

    private void Start()
    {
        Card();
    }
}
