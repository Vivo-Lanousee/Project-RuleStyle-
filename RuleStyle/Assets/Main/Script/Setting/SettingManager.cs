using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum SettingOriginator
{
    none = -1,
    AudioVolume = 0
}

public class SettingManager : SingletonMonoBehaviourBase<SettingManager>
{
    /// <summary>
    /// 0 = �ݒ��ʂ��J���Ă��� / 
    /// </summary>
    BitArray settingstatus = new BitArray(2,false);

    List<SettingBT> settingBTs = new List<SettingBT>();

    List<SettingType> valueChanged = new List<SettingType>();

    // �l���ύX���ꂽ�Ώ�
    BitArray valueChangedCheck = new BitArray(32, false);
    BitArray All = new BitArray(32, false);


    // ���ݕύX�\�������݂��镨
    List<object> loadSettled = new List<object>(new object[32]);

    // �ۑ��Ώ�
    BitArray saveTarget = new BitArray(32, false);

    // loadSettled�ɏd�����Ă���Ȃ��悤�ɂ���
    BitArray SObit = new BitArray(32,false);
    // ���[�h���������Ă��邩�ǂ���
    BitArray SOLoadSettledbit = new BitArray(32,false);

    int num = -1;
    #region SetGetReset
    public int SetSettingBTs(SettingBT bt) {  settingBTs.Add(bt); settingstatus[0] = true; num++; return num; }

    public void SetChanged(int num,bool check) { valueChangedCheck[num] = check; }

    public void SetChangedValue(SettingType ST) { valueChanged.Add(ST); }
    public void SetLoadSettled(SettingOriginator SO)
    {
        if (!SObit[(int)SettingOriginator.AudioVolume])
        {
            Type type = Type.GetType(SO.ToString());
            loadSettled[(int)SettingOriginator.AudioVolume] = Activator.CreateInstance(type);
        }
        else
        {
            SObit[(int)SettingOriginator.AudioVolume] = true;
        }
    }

    public bool GetValueChangedCheck() { return !(valueChangedCheck.Cast<bool>().SequenceEqual(All.Cast<bool>())); }

    public void ReseAllt()
    {
        settingstatus[0] = false;
        ResetSettingBTs();
        ResetChangedValue();
        ResetLoadSettled();
    }

    public void ResetSettingBTs() { settingBTs = new List<SettingBT>(); num = -1; valueChangedCheck = ResetSObitAll(); }
    public void ResetChangedValue() { valueChanged = new List<SettingType>(); }
    public void ResetLoadSettled()
    {
        loadSettled = new List<object>(new object[32]);
        SObit = ResetSObitAll();
        SOLoadSettledbit = ResetSObitAll();
        ResetLoadTarget();
    }

    public void ResetLoadTarget() { saveTarget = ResetSObitAll(); }

    private BitArray ResetSObitAll() { return new BitArray(SOLoadSettledbit.Count, false); }

    #endregion

    /// <summary>
    /// ��������
    /// </summary>
    public SettingOriginator SearchSaveSlot(SettingType ST)
    {
        switch (ST)
        {
            case SettingType.BGMVolume:
            case SettingType.SEVolume:
            case SettingType.BGMmute:
            case SettingType.SEmute:
                return SettingOriginator.AudioVolume;
        }
        return SettingOriginator.none;
    }

    /// <summary>
    /// �l���擾
    /// </summary>
    public float GetValue(SettingType ST)
    {
        switch (SearchSaveSlot(ST))
        {
            case SettingOriginator.AudioVolume:
                if (!SOLoadSettledbit[(int)SettingOriginator.AudioVolume])
                {
                    Load(SettingOriginator.AudioVolume);
                }

                AudioManager audioManager = AudioManager.Instance();

                switch (ST)
                {
                    case SettingType.BGMVolume:
                        return audioManager.GetVolume.BGMVolume;
                    case SettingType.SEVolume:
                        return audioManager.GetVolume.SEVolume;
                    case SettingType.BGMmute:
                        return (audioManager.GetVolume.BGMmute) ? 1 : 0;
                    case SettingType.SEmute:
                        return (audioManager.GetVolume.SEmute) ? 1 : 0;
                }
                break;
        }

        return 0;
    }


    /// <summary>
    /// ���[�h�����m�F
    /// </summary>
    public bool Search_LoadCompletionCheck(SettingType ST)
    {
        switch (SearchSaveSlot(ST))
        {
            case SettingOriginator.AudioVolume:
                return SOLoadSettledbit[(int)SettingOriginator.AudioVolume];
        }
        return false;
    }

    public void Load(SettingType ST) { Load(SearchSaveSlot(ST)); }
    public void Load(SettingOriginator SO)
    {
        if (!SOLoadSettledbit[(int)SettingOriginator.AudioVolume])
        {
            string filePath = Application.persistentDataPath + "/" + SO.ToString() + ".json";  // �ۑ���p�X

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);  // �t�@�C������ǂݍ���
                switch (SO)
                {
                    case SettingOriginator.AudioVolume:
                        AudioManager audioManager = AudioManager.Instance();
                        audioManager.SetLoad(JsonUtility.FromJson<AudioVolume>(json));
                        
                        SOLoadSettledbit[(int)SettingOriginator.AudioVolume] = true;
                        SetLoadSettled(SO);
                        
                        break;
                }


            }
        }
        else
        {
            Debug.Log("No saved data found.");
        }
    }


    public void Save() { StartCoroutine(SaveWait()); }

    // �ύX���ꂽ�f�[�^��JSON�t�@�C���Ƃ��ĕۑ�
    private void Save<T>(T save, SettingOriginator SO) where T : class
    {
        SOLoadSettledbit[(int)SettingOriginator.AudioVolume] = false;

        // JSON�ɃV���A���C�Y
        string json = JsonUtility.ToJson(save, true);

        // Addressables�̕ۑ��ꏊ�̃p�X�i�K�v�ȃf�B���N�g�����쐬�j
        string filePath = Application.persistentDataPath + "/" + SO.ToString() + ".json";

        // �f�B���N�g�������݂��Ȃ��ꍇ�͍쐬����
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // JSON�f�[�^���t�@�C���ɏ�������
        File.WriteAllText(filePath, json, Encoding.UTF8);
        Debug.Log("JSON saved to: " + filePath);

        Load(SO);
    }

    IEnumerator SaveWait()
    {
        settingstatus[1] = true;
        for (int i = 0; i < settingBTs.Count; i++)
        {
            if (valueChangedCheck[i])
            {
                settingBTs[i].ValueReset();
                SOLoadSettledbit[(int)SearchSaveSlot(settingBTs[i].GetSettingType)] = false;
                saveTarget[(int)SearchSaveSlot(settingBTs[i].GetSettingType)] = true;
                // �^�����擾
                Type type = loadSettled[(int)SearchSaveSlot(settingBTs[i].GetSettingType)].GetType();

                // �t�B�[���h���擾
                FieldInfo fieldInfo = type.GetField(settingBTs[i].GetSettingType.ToString(), BindingFlags.Public | BindingFlags.Instance);

                if (fieldInfo != null)
                {
                    switch (settingBTs[i].GetSettingType)
                    {
                        case SettingType.BGMVolume:
                        case SettingType.SEVolume:
                            // �t�B�[���h�ɐV�����l����
                            fieldInfo.SetValue(loadSettled[(int)SearchSaveSlot(settingBTs[i].GetSettingType)], settingBTs[i].GetValue);
                            break;
                        case SettingType.BGMmute:
                        case SettingType.SEmute:
                            // �t�B�[���h�ɐV�����l����
                            fieldInfo.SetValue(loadSettled[(int)SearchSaveSlot(settingBTs[i].GetSettingType)], (settingBTs[i].GetValue == 1) ? true : false);
                            break;
                    }
                }
            }
        }

        for (int i = 0; i < loadSettled.Count; i++)
        {
            if (saveTarget[i])
            {
                Save(loadSettled[i], (SettingOriginator)i);
            }
        }

        settingstatus[1] = false;
        yield return 0;

        ResetLoadTarget();
    }


}
