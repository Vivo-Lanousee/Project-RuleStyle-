using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDis
{
    right,left
}

public class RotationUI : MonoBehaviour
{   
    List<RectTransform> rotationUI = new List<RectTransform>();

    Vector2 dspsize = new Vector2(Screen.width, Screen.height);

    [SerializeField, Header("��ʔ䗦")]
    float percent = 0.8f;

    [SerializeField, Header("�����܂ł̎���")]
    float timeToComplete = 2;
    float timer = 0;
    //[SerializeField,Header("��]����")]
    RotationDis rotationDis = RotationDis.right;

    float[] DirectionsData = new float[4] { 180 , 90, 0 , 270 };
    List<float> Directions = new List<float>();

    int playerNumber = 0;

    /// <summary>
    /// 0 = �l���擾���� / 1 = rotationUI�擾���� / 2 = ��]���s��ԊǗ�
    /// </summary>
    BitArray bit = new BitArray(4, false);

    private void Awake()
    {
        StartCoroutine(SetDirections());
    }

    /// <summary>
    /// UI �� RectTransform�������ő��
    /// </summary>
    /// <param name="inRotationUI"></param>
    /// <param name="num"></param>
    public void InRotationUI(RectTransform inRotationUI,int num)
    {
        StartCoroutine(InRotationUIWait(inRotationUI, num));
    }

    /// <summary>
    /// �l�����擾�ł���܂őҋ@
    /// </summary>
    /// <param name="inRotationUI"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    IEnumerator InRotationUIWait(RectTransform inRotationUI, int num)
    {
        // �l�����擾�ł���܂őҋ@
        yield return new WaitUntil(() => bit[0]);
        if(num <= rotationUI.Count)
        {
            rotationUI[num - 1] = inRotationUI;
            playerNumber++;
        }
    }

    /// <summary>
    /// �K�v�ȕ����擾�ł���܂őҋ@
    /// </summary>
    /// <returns></returns>
    IEnumerator SetDirections()
    {
        // �����Ől�����擾���Ă���
        yield return new WaitUntil(() => true);
        rotationUI = new List<RectTransform>(new RectTransform[4]);
        bit[0] = true;
        Debug.Log("RotationUI �l���ݒ芮�� /num:" + rotationUI.Count);

        switch (rotationUI.Count)
        {
            case 4:
                DirectionsData = ((rotationDis == RotationDis.right) ? new float[4] { 180, 90, 0, 270 } : new float[4] { 180,270, 0, 90});
                break;
            case 3:
                DirectionsData = ((rotationDis == RotationDis.right) ? new float[3] { 180, 90, 270 } : new float[3] { 180,270, 90 });
                break;
            case 2:
                DirectionsData =  new float[2] { 180, 0 };
                break;
        }

        // ������UI����RectTransform��l�����󂯎��܂ő҂�
        yield return new WaitUntil(() => (rotationUI.Count == playerNumber) ? true : false);
        Debug.Log("RotationUI RectTransform �󂯎�芮��");
        RotationSet();
        bit[1] = true;
    }

    public void StartRotation()
    {
        if (!bit[2]) { StartCoroutine(StartRotationWait()); }
    }

    IEnumerator StartRotationWait()
    {
        bit[2] = true;

        yield return new WaitUntil(() => bit[1]);

        Debug.Log("RotationUI ��]�J�n");

        timer = 0;
        

        StartCoroutine(OnRotation());
    }

    IEnumerator OnRotation()
    {
        yield return new WaitForSeconds(1 / 30);
        timer = (timer + Time.deltaTime > timeToComplete) ? timeToComplete : timer + Time.deltaTime;
        bit[2] = (timer == timeToComplete) ? false:true;
        // �t�F�[�h�C��/�A�E�g�䗦�ݒ�
        float fadePerc = Mathf.Abs((timer / timeToComplete));

        float dis = ((Mathf.PI * 0.5f) * fadePerc) * ((rotationDis == RotationDis.right) ? 1 : -1);

        RotationSet(dis);


        if (bit[2])
        {
            StartCoroutine(OnRotation());
        }
        else
        {
            ResetRotationUI();
        }

    }

    public void ResetRotationUI()
    {
        RectTransform zerorect = rotationUI[0];

        for (int i = 0;i< rotationUI.Count-1; i++)
        {
            rotationUI[i] = rotationUI[i + 1];
        }

        rotationUI[rotationUI.Count - 1] = zerorect;
    }

    void RotationSet(float dis = 0)
    {
        Debug.Log("RotationSet �X�^�[�g");
        for (int i = 0; i < rotationUI.Count; i++)
        {
            Vector2 vec = new Vector2(Mathf.Sin((DirectionsData[i] * Mathf.Deg2Rad) + dis), Mathf.Cos((DirectionsData[i] * Mathf.Deg2Rad) + dis)) * ((dspsize/2) * percent);
            vec += dspsize / 2;
            Debug.Log("RotationSet/x:"+vec.x+"/y:"+vec.y);
            rotationUI[i].position = vec;
        }
    }

}
