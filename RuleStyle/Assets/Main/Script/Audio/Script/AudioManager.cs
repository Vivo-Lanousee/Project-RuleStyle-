using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

// �����^�C�v�̗񋓌^�BBGM�i�w�i���y�j��SE�i���ʉ��j�̎�ނ��w��
public enum AudioType
{
    BGM, SE,        // �w�i���y�ƌ��ʉ�
    NextBGM, NextSE  // ����BGM�Ǝ���SE
}

// ���ʐݒ���i�[����N���X
[System.Serializable]
public class AudioVolume
{
    [SerializeField]
    public float BGMVolume = 1;  // BGM�̉��ʁi�f�t�H���g�͍ő�j
    [SerializeField]
    public float SEVolume = 1;   // SE�̉��ʁi�f�t�H���g�͍ő�j
    [SerializeField]
    public bool BGMmute = true;  // BGM�̃~���[�g�ݒ�i�f�t�H���g�̓~���[�g�j
    [SerializeField]
    public bool SEmute = true;   // SE�̃~���[�g�ݒ�i�f�t�H���g�̓~���[�g�j
}

// �����Ǘ����s���N���X
public class AudioManager : SingletonMonoBehaviourBase<AudioManager>
{
    // �eAudioSource��ێ�����ϐ�
    AudioSource BGMSource;
    AudioSource NextBGMSource;
    AudioSource SESource;
    AudioSource NextSESource;

    // ���ʐݒ��ێ�����ϐ�
    AudioVolume volumes;

    // �Q�[���J�n���̏���������
    private void Start()
    {
        // �ݒ�����[�h
        SettingManager settingManager = SettingManager.Instance();
        settingManager.Load(SettingOriginator.AudioVolume);

        // AudioSource��ǉ�
        BGMSource = gameObject.AddComponent<AudioSource>();
        SESource = gameObject.AddComponent<AudioSource>();
        NextBGMSource = gameObject.AddComponent<AudioSource>();
        NextSESource = gameObject.AddComponent<AudioSource>();

        // ���ʐݒ�𔽉f
        SetVolume();
    }

    // ���ʐݒ��K�p���郁�\�b�h
    public void SetVolume() { StartCoroutine(SetVolumes()); }

    // ���ʐݒ�𔽉f������R���[�`��
    IEnumerator SetVolumes()
    {
        // BGMSource���ݒ肳���̂�ҋ@
        yield return new WaitUntil(() => (BGMSource != null) ? true : false);
        BGMSource.volume = (volumes.BGMmute) ? volumes.BGMVolume : 0;
        BGMSource.loop = true;  // BGM�̓��[�v����ݒ�

        // NextBGMSource���ݒ肳���̂�ҋ@
        yield return new WaitUntil(() => (NextBGMSource != null) ? true : false);
        NextBGMSource.volume = (volumes.BGMmute) ? volumes.BGMVolume : 0;
        NextBGMSource.loop = true;

        // SESource���ݒ肳���̂�ҋ@
        yield return new WaitUntil(() => (SESource != null) ? true : false);
        SESource.volume = (volumes.SEmute) ? volumes.SEVolume : 0;
        SESource.loop = false;  // SE�̓��[�v���Ȃ��ݒ�

        // NextSESource���ݒ肳���̂�ҋ@
        yield return new WaitUntil(() => (NextSESource != null) ? true : false);
        NextSESource.volume = (volumes.SEmute) ? volumes.SEVolume : 0;
        NextSESource.loop = false;
    }

    // ���y���t�F�[�h�A�E�g���郁�\�b�h
    public void AudioFadeOut(float maxTime) { AudioFade(maxTime, FadeSpecified._1to0); }

    // ���y���t�F�[�h�C�����郁�\�b�h
    public void AudioFadeIn(float maxTime) { AudioFade(maxTime, FadeSpecified._0to1); }

    // ���ʃt�F�[�h����
    public void AudioFade(float maxTime, FadeSpecified fade)
    {
        Time_TimerManager time_TimerManager = Time_TimerManager.Instance();
        time_TimerManager.Fade(FadeVolumeWait, maxTime, fade); // �t�F�[�h���������s
    }

    // �t�F�[�h�̑ҋ@����
    void FadeVolumeWait(float fade)
    {
        // �t�F�[�h�ɉ��������ʂ�ݒ�
        BGMSource.volume = (volumes.BGMmute) ? volumes.BGMVolume * fade : 0;
    }

    // ���ʐݒ���㏑�����郁�\�b�h
    public void SetLoad(AudioVolume sstVolume) { volumes = sstVolume; SetVolume(); }

    // ���݂̉��ʐݒ���擾����v���p�e�B
    public AudioVolume GetVolume { get { return volumes; } }

    // BGM��ݒ肷��v���p�e�B
    public AudioClip SetBGM { set { BGMSource.clip = value; } }

    // ����BGM��ݒ肷��v���p�e�B
    public AudioClip SetNextBGM { set { NextBGMSource.clip = value; } }

    // BGM���Đ����郁�\�b�h
    public void PlayBGM() { StartCoroutine(Check(AudioType.BGM)); }

    // SE��ݒ肷��v���p�e�B
    public AudioClip SetSE { set { SESource.clip = value; } }

    // ����SE��ݒ肷��v���p�e�B
    public AudioClip SetNextSE { set { NextSESource.clip = value; } }

    // SE���Đ����郁�\�b�h
    public void PlaySE() { StartCoroutine(Check(AudioType.SE)); }

    // �w�肵�������^�C�v���Đ����郁�\�b�h
    public void Play(AudioType type) { StartCoroutine(Check(type)); }

    /// <summary>
    /// AudioType�ɉ����ĉ����̍Đ�������
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IEnumerator Check(AudioType type)
    {
        yield return 0;

        switch (type)
        {
            case AudioType.BGM:
                if (BGMSource.clip == null)
                {
                    // �ŏ���BGM�����[�h
                    LoadFirst("firstBGM", type);
                    yield return new WaitUntil(() => (BGMSource.clip != null) ? true : false);
                }
                BGMSource.Play();  // BGM���Đ�
                break;
            case AudioType.SE:
                if (SESource.clip == null)
                {
                    // �ŏ���SE�����[�h
                    LoadFirst("firstSE", type);
                    yield return new WaitUntil(() => (SESource.clip != null) ? true : false);
                }
                SESource.Play();  // SE���Đ�
                break;
            case AudioType.NextBGM:
                // ����BGM�͂܂������Ȃ�
                break;
            case AudioType.NextSE:
                if (NextSESource.clip == null)
                {
                    // ����SE�����[�h
                    LoadFirst("nextSE", type);
                    yield return new WaitUntil(() => (NextSESource.clip != null) ? true : false);
                }
                NextSESource.Play();  // ����SE���Đ�
                break;
        }
    }

    /// <summary>
    /// �ŏ��̉����f�[�^�iBGM/SE�j�����[�h���郁�\�b�h
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    private void LoadFirst(string name, AudioType type)
    {
        // Addressables���g���Ĕ񓯊���AudioClip�����[�h
        AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(name);

        // ���[�h������̏���
        handle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                // ���[�h�������ɑΉ�����AudioClip��ݒ�
                switch (type)
                {
                    case AudioType.BGM:
                        SetBGM = op.Result;
                        break;
                    case AudioType.SE:
                        SetSE = op.Result;
                        break;
                    case AudioType.NextBGM:
                        SetNextBGM = op.Result;
                        break;
                    case AudioType.NextSE:
                        SetNextSE = op.Result;
                        break;
                }
            }
            else
            {
                // ���[�h���s���ɃG���[���O��\��
                Debug.LogError("AudioClip�̃��[�h�Ɏ��s���܂���");
            }
        };
    }
}