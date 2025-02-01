using System;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public enum FadeSpecified
{
   notSpecified = 0,
   _0to1 = 1,
   _1to0 = -1
}

public class FrameRate
{
    float frameRate = 1 / 240;
    public float GetFrameRate {  get { return frameRate; } }
}

/// <summary>
/// ���Ԏ��J��Ԃ�������
/// </summary>
public class Time_TimerManager : SingletonMonoBehaviourBase<Time_TimerManager>
{
    FrameRate frameRate = new FrameRate();

    public float GetframeRate { get { return frameRate.GetFrameRate; } }
    float timer = 0;

    private void Awake()
    {
        StartCoroutine(TimerCount());
    }

    IEnumerator TimerCount()
    {
        yield return new WaitForSeconds(frameRate.GetFrameRate);
        timer += Time.deltaTime;
    }

    #region �t�F�[�h�C��/�A�E�g
    /// <summary>
    /// �w�肳�ꂽ�֐���p���ăt�F�[�h�C��/�t�F�[�h�A�E�g�������s��
    /// </summary>
    /// <param name="call"></param>
    /// <param name="maxTime"></param>
    public void Fade(UnityAction<float> call,float maxTime, FadeSpecified fade = FadeSpecified.notSpecified)
    {
        // �t�F�[�h������ݒ�
        // maxTime�@�������Ȃ� 0->1 �̃t�F�[�h�C��
        // maxTime�@�������Ȃ� 1->0 �̃t�F�[�h�A�E�g
        if (fade != FadeSpecified.notSpecified) { maxTime = Mathf.Clamp01(maxTime) * fade.GetHashCode(); }
        StartCoroutine (FadeWait(call, maxTime , MathF.Abs(maxTime)));
    }


    /// <summary>
    /// �t�F�[�h�C��/�A�E�g�̌J��Ԃ�/�䗦�v�Z/�֐����s���߂������Ȃ�
    /// </summary>
    /// <param name="call"></param>
    /// <param name="maxTime"></param>
    /// <param name="AbsmaxTime"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator FadeWait(UnityAction<float> call, float maxTime , float AbsmaxTime, float time = 0)
    {
        yield return new WaitForSeconds(frameRate.GetFrameRate);

        // �ő�l�𒴂��Ȃ��悤�ɂ��J�E���g
        time = (time + Time.deltaTime < AbsmaxTime) ? time + Time.deltaTime : AbsmaxTime;

        // �t�F�[�h�C��/�A�E�g�̔䗦�ݒ� �� �����Ƃ��Đݒ�
        object[] fadePerc = { Mathf.Abs((time / AbsmaxTime) - ((maxTime <= 0) ? 1 : 0)) };

        // �w�肳�ꂽ�֐������s����֐�
        FadeMethod(call, fadePerc);

        if (time != AbsmaxTime) { StartCoroutine(FadeWait( call , maxTime , AbsmaxTime , time )); }
    }

    /// <summary>
    /// �w�肳�ꂽ�֐��Ƀt�F�[�h�䗦��ԊҎ��s����
    /// </summary>
    /// <param name="call"></param>
    /// <param name="fadePerc"></param>
    /// <returns></returns>
    object FadeMethod(UnityAction<float> call, object[] fadePerc)
    {
        try { return call.Method.Invoke(call.Target, fadePerc); }
        catch (Exception ex) { Debug.LogError($"Error: {ex.Message}"); return null; }
    }

    #endregion

}
