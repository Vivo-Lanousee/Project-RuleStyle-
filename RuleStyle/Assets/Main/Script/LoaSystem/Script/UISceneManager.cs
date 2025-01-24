using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement; // Task���g�����߂ɕK�v

public class UISceneManager : SingletonMonoBehaviourBase<UISceneManager>
{

    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName.ToString(), LoadSceneMode.Additive);
    }

    #region UI
    // ���ݏo�����Ă���UI
    BitArray nowUIAdvents = new BitArray(32,false);

    // UI���j���[�ꗗ(�菑��)
    private Call[] uinames = new Call[] { Call.Menu , Call.VolumeSetting };

    // �o�����Ă���UI���j���[�̈ꗗ
    List<Call> lastUImenuScene = new List<Call>();

    List<Call> UIScenes = new List<Call>();

    // ��ԍŌ��Maine(�^�C�g����Q�[��scene)scene
    public Call lastMainScene = Call.TitleTest;

    void Advent(Call call)
    {
        lastUImenuScene.Add(call); Debug.Log("ua/" + lastUImenuScene.Count);
        UnityEngine.SceneManagement.SceneManager.LoadScene(call.ToString(), LoadSceneMode.Additive);
    }

    void Delete(Call call)
    {
        UIMenuDelete(call);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(call.ToString());
    }

    public void UISceneAdvent(Call call)
    {
        UIScenes.Add(call); Debug.Log("ua/" + UIScenes.Count);
        UnityEngine.SceneManagement.SceneManager.LoadScene(call.ToString(), LoadSceneMode.Additive);
    }

    public void UISceneDelete(Call call)
    {
        UIDelete(call);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(call.ToString());
    }

    #region Call ( 0 )
    public void CallAdvent(Call call)
    {
        if (call == Call.Now) { call = GetLast(); }
        for (int i = 0; i < uinames.Length; i++)
        {
            if (!nowUIAdvents[i])
            {
                nowUIAdvents[i] = true;
                Advent(call);
                return;
            }
        }
    }

    public void CallDelete(Call call)
    {
        if (call == Call.Now) { call = GetLast(); }
        for (int i = 0; i < uinames.Length; i++)
        {
            if (call == uinames[i]) {
                
                Debug.Log("Del");
                nowUIAdvents[i] = false;
                Delete(call);

                return;
            } 
        }


        
    }
    #endregion

    public void ChangeScene(Call call)
    {
        Call call2 = lastMainScene;
        ResetScene();
        LoadIn(call, call2);
        
    }

    /// <summary>
    /// 1:true:��~/faluse:�����
    /// </summary>
    private BitArray TimeChange = new BitArray(1, false);

    void TimeStop() { TimeChange[0] = true; Time.timeScale = 0; }
    void TimeReStart() { TimeChange[0] = false; Time.timeScale = 1; }
    public bool GetTimeChange() { return TimeChange[0]; }

    public Call GetLast()
    {
        if(lastUImenuScene.Count > 0) { return lastUImenuScene[lastUImenuScene.Count - 1]; }
        return lastMainScene;
    }

    public Call GetUILast()
    {
        if (lastUImenuScene.Count > 0) { return lastUImenuScene[lastUImenuScene.Count - 1]; }
        return lastMainScene;
    }

    public bool Search(Call search)
    {
        for (int i = 0; i < lastUImenuScene.Count; i++)
        {
            Debug.Log(i);
            if(search == lastUImenuScene[i])
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckUI()
    {
        if (lastUImenuScene.Count == 0) { return false; }
        return true;
    }

    public void UIMenuDelete(Call call)
    {
        List<Call> list = new List<Call>(lastUImenuScene);
        int x = list.Count - 1;
        Debug.Log("ud/" + lastUImenuScene.Count + "/" + x + "/" +((lastUImenuScene.Count >= 1) ? lastUImenuScene[0] : "") + "/" + ((lastUImenuScene.Count >= 2) ? lastUImenuScene[1] :""));
        lastUImenuScene.Clear();
        Debug.Log(list.Count);

        int s = 0;

        for (int i = 0; i < x; i++)
        {
            if (list[i + s] == call) { s++; continue; }
            Debug.Log(i + s);
            lastUImenuScene.Add(list[i+s]);
        }

        Debug.Log("ud/" + lastUImenuScene);

        if (lastUImenuScene.Count <= 0) { Debug.Log("udrs"); TimeReStart(); }
    }

    public void UIDelete(Call call)
    {
        List<Call> list = new List<Call>(UIScenes);
        int x = list.Count - 1;
        Debug.Log("ud/" + UIScenes.Count + "/" + x + "/" + ((UIScenes.Count >= 1) ? UIScenes[0] : "") + "/" + ((UIScenes.Count >= 2) ? UIScenes[1] : ""));
        UIScenes.Clear();
        Debug.Log(list.Count);

        int s = 0;

        for (int i = 0; i < x; i++)
        {
            if (list[i + s] == call) { s++; continue; }
            Debug.Log(i+s);
            UIScenes.Add(list[i+s]);
        }

        Debug.Log("ud/" + UIScenes);

        if (UIScenes.Count <= 0) { Debug.Log("udrs"); TimeReStart(); }
    }

    public void ResetScene()
    {

        Debug.Log("RS");
        foreach (Call call in lastUImenuScene)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(call.ToString());
        }
        foreach (Call call in UIScenes)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(call.ToString());
        }

        lastUImenuScene.Clear();
        UIScenes.Clear();
        TimeReStart();
        nowUIAdvents = new BitArray(32, false);
    }

    #endregion

    //------------------------------------------------------------------------------------------------------------------------------------------------------
    //
    //------------------------------------------------------------------------------------------------------------------------------------------------------

    #region ���[�h�V�[��

    Call LoadSceneName = Call.Loadscene;


    /// <summary>
    /// 0 = LoadSceneManager���ł̒l | 1 = loadscene���ł̒l
    /// </summary>
    BitArray LoadStatus = new BitArray(2, false);

    public void SetLoad(bool xxx) { LoadStatus[1] = xxx; }
    public bool GetLoad() { return LoadStatus[0]; }


    /// <summary>
    /// �ʓrloadscene��ύX����ꍇ
    /// </summary>
    /// <param name="key"></param>
    public void SetLoadScene(Call key)
    {
        int sceneCount = SceneManager.sceneCount;

        bool Load = false;

        for (int i = 0; i < sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == LoadSceneName.ToString()) { Load = true; break; }
        }

        if (Load)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(LoadSceneName.ToString());
        }

        LoadSceneName = key;
        UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName.ToString(), LoadSceneMode.Additive);
    }


    /// <summary>
    /// load�J�n
    /// </summary>
    public void LoadIn(Call NextSceneName, Call CloseSceneName)
    {

        LoadStatus[0] = true;

        int sceneCount = SceneManager.sceneCount;

        bool Load = true;

        for (int i = 0; i < sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == LoadSceneName.ToString()) { Load = false; break; }
        }

        if (Load)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName.ToString(), LoadSceneMode.Additive);
        }

        if(CloseSceneName == Call.Now) { CloseSceneName = lastMainScene; }

        StartCoroutine(LoadSetupWait(NextSceneName, CloseSceneName));
        lastMainScene = NextSceneName;
    }

    /// <summary>
    /// ���[�h�V�[�����N���������Ƃ��m�F����܂ő҂�
    /// </summary>
    /// <param name="NextSceneName"></param>
    /// <param name="CloseSceneName"></param>
    /// <returns></returns>
    IEnumerator LoadSetupWait(Call NextSceneName, Call CloseSceneName)
    {
        yield return new WaitUntil(() => LoadStatus[1]);

        UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName.ToString(), LoadSceneMode.Additive);

        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(CloseSceneName.ToString());
    }

    /// <summary>
    /// load�I���w��
    /// </summary>
    public void LoadOut()
    {
        //���[�h��Ԃ̃��Z�b�g
        LoadStatus[0] = false;
    }

    #endregion
}

