using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOut : MonoBehaviour
{
    [SerializeField]
    AudioClip BGMclip;

    [SerializeField]
    float waitTime = 1.0f;

    [SerializeField]
    private Call[] SetUIs = new Call[0];
    // objectの有効化時
    private void OnEnable()
    {
        UISceneManager uISceneManager = UISceneManager.Instance();

        foreach (Call call in SetUIs) { uISceneManager.UISceneAdvent(call); }

        uISceneManager.LoadOut();

        StartCoroutine(StartBGMWait());
    }

    IEnumerator StartBGMWait()
    {
        AudioManager audioManager = AudioManager.Instance();

        yield return new WaitUntil(() => (audioManager != null) ? true:false);

        audioManager.SetBGM = BGMclip;
        // ロードシーンの状態を確認
        UISceneManager loadSceneManager = UISceneManager.Instance();
        
        yield return new WaitUntil(() => !loadSceneManager.GetLoad());
        
        yield return new WaitForSeconds(waitTime);
        audioManager.PlayBGM();

    }
}
