using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetCardUI : MonoBehaviour
{
    [SerializeField]
    Button cardGetEventEndBT;

    [SerializeField]
    Button startmaskbt;

    [SerializeField]
    RectMask2D mask;

    [SerializeField]
    GameObject cardprefab;
    [SerializeField]
    RectTransform cardSlide;


    [SerializeField]
    Button Left;

    [SerializeField]
    Button Right;

    int Allcardslidenumber = 1;

    int cardMaxnum = 5;

    List<RectTransform> cards = new List<RectTransform>();

    Vector3[] cardPositions = {
        new Vector3(Screen.width * 2, Screen.height * 2,0),
        new Vector3((Screen.width / 3) * 0.5f,Screen.height*0.55f,0),
        new Vector3((Screen.width / 3) * 1.5f, Screen.height*0.55f,0),
        new Vector3((Screen.width / 3) * 2.5f, Screen.height*0.55f,0),
        new Vector3((Screen.width / 3) * 3.5f, Screen.height*0.55f,0),
        new Vector3((Screen.width / 3) * 4.5f, Screen.height*0.55f,0), };

    int[][] cardPosSet = {
        new int[] { 0 },
        new int[] { 2 }, 
        new int[] { 1 , 3 }, 
        new int[] { 1 , 2 , 3 }, 
        new int[] { 1 , 2 , 3 , 4 }, 
        new int[] { 1 , 2 , 3 , 4 , 5 } };

    float right = Screen.width * -1;

    [SerializeField, Header("�����܂ł̎���")]
    float timeToComplete = 2;
    float fadetimer = 0;

    Vector2 dspsize = new Vector2(Screen.width, Screen.height);

    BitArray bit = new BitArray(4,false);
    private void Awake()
    {
        cardGetEventEndBT.transform.gameObject.SetActive(false);
        MaskReset();
        startmaskbt.onClick.AddListener(GetCardEventResetStart);
        StartCoroutine(CardsSet());
        CardSlideBTSet();
    }

    void GetCardEventResetStart()
    {
        cardGetEventEndBT.transform.gameObject.SetActive(false);
        fadetimer = 0;
        MaskReset();
        CardSlideBTSet();
        StartFade();
    }



    IEnumerator CardsSet()
    {
        for (int i = 0; i < cardMaxnum; i++)
        {
            // �I�u�W�F�N�g�𐶐�
            GameObject spawnedObject = Instantiate(cardprefab);

            // �e�I�u�W�F�N�g�̎q�Ƃ��Đݒ�
            spawnedObject.transform.SetParent(cardSlide);

            cards.Add(spawnedObject.GetComponent<RectTransform>());
        }
        yield return new WaitUntil(()=>cards.Count == cardMaxnum);
        CardPositionSet(cardMaxnum);
    }

    void CardPositionSet(int num)
    {
        for (int i = 0;i < cards.Count; i++)
        {
            cards[i].transform.position = cardPositions[0];
        }

        if (num == 0) { return; }
        foreach (int i in cardPosSet[num])
        {
            cards[i-1].position = cardPositions[i];
        }

        Allcardslidenumber = (int)(num/3);
    }

    /// <summary>
    /// mask��reset����
    /// </summary>
    public void MaskReset()
    {
        mask.padding = new Vector4(0, dspsize.y, right, -300);
        cardSlide.position = new Vector3(Screen.width/2, Screen.height/2, 0);
        cardGetEventEndBT.transform.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// mask��off�ɂ���
    /// </summary>
    public void NoneMask()
    {
        cardGetEventEndBT.transform.gameObject.SetActive(true);
        mask.padding = new Vector4(0, -300, right, -300);
    }

    public void StartFade()
    {
        StartCoroutine(OnFade());
    }

    IEnumerator OnFade()
    {
        yield return new WaitForSeconds(1 / 30);
        fadetimer = (fadetimer + Time.deltaTime > timeToComplete) ? timeToComplete : fadetimer + Time.deltaTime;
        bit[2] = (fadetimer == timeToComplete) ? false : true;
        // �t�F�[�h�C��/�A�E�g�䗦�ݒ�
        float fadePerc = Mathf.Abs((fadetimer / timeToComplete) - 1);

        float dis = ((dspsize.y + 300f) * fadePerc) - 300f;

        mask.padding = new Vector4(0, dis, right, -300);

        if (bit[2])
        {
            StartCoroutine(OnFade());
        }
        else
        {
            cardGetEventEndBT.transform.gameObject.SetActive(true);
            cardGetEventEndBT.onClick.AddListener(CardChangeSystemAdd);
        }

    }

    /// <summary>
    /// ������card���m�F���card�����ς���scene���Ăяo������
    /// </summary>
    void CardChangeSystemAdd()
    {
        MaskReset();
        UISceneManager uISceneManager = UISceneManager.Instance();
        uISceneManager.CallAdvent(Call.GameUIchangeCard);

    }


    BitArray LeftRight = new BitArray(4, false);

    float MovePerc = 0;

    float time = 0;

    float maxTime = 1;

    float allmaxTime = 0;



    // �J�[�h�X���C�h�̃{�^���ݒ�
    void CardSlideBTSet()
    {
        // �E�{�^���̃N���b�N���X�i�[�ݒ�
        if (allmaxTime != Allcardslidenumber) { Right.onClick.AddListener(() => CardSlide(RotationDis.right)); }
        else { Right.onClick.RemoveAllListeners(); }

        // ���{�^���̃N���b�N���X�i�[�ݒ�
        if (allmaxTime != 0) { Left.onClick.AddListener(() => CardSlide(RotationDis.left)); }
        else { Left.onClick.RemoveAllListeners(); }
    }

    /// <summary>
    /// �J�[�h�̈ꗗ�����E�Ɉړ������Ċm�F����
    /// </summary>
    void CardSlide(RotationDis dis)
    {
        // �ړ�������ݒ�
        LeftRight[(dis == RotationDis.right) ? 1 : 0] = true;
        LeftRight[(dis != RotationDis.right) ? 1 : 0] = false;

        // �S�̂̍ő�X���C�h���Ԃ̍X�V
        allmaxTime = (Mathf.Abs(allmaxTime + dis.GetHashCode()) <= Allcardslidenumber) ? allmaxTime + dis.GetHashCode() : Allcardslidenumber;
        allmaxTime = (allmaxTime >= 0) ? allmaxTime : 0;


        CardSlideBTSet();

        // �t�F�[�h�����̂��߂ɍ������ƉE�����̃t���O���X�V
        LeftRight[3] = LeftRight[2];
        StartCoroutine(CardSlidechange(dis));
    }

    // �t�F�[�h������ɃX���C�h���������s����R���[�`��
    IEnumerator CardSlidechange(RotationDis dis)
    {
        // ���E�̃X���C�h���I���܂őҋ@
        yield return new WaitUntil(() => !LeftRight[3]);
        StartCoroutine(CardSlideWait());
        LeftRight[2] = true;
    }

    // �t�F�[�h���������s����R���[�`��
    IEnumerator CardSlideWait()
    {
        // �^�C�}�[���C���X�^���X�����ăt���[�����[�g�Ɋ�Â��đҋ@
        Time_TimerManager time_TimerManager = Time_TimerManager.Instance();
        yield return new WaitForSeconds(time_TimerManager.GetframeRate);

        Debug.Log(Time.deltaTime);

        // �ő�l�𒴂��Ȃ��悤�ɃJ�E���g��i�߂�
        if (LeftRight[1]) { time = (time + Time.deltaTime < allmaxTime) ? time + Time.deltaTime : allmaxTime; }
        if (LeftRight[0]) { time = (time - Time.deltaTime > allmaxTime) ? time - Time.deltaTime : allmaxTime; }

        // �t�F�[�h�C��/�A�E�g�̔䗦�ݒ�
        MovePerc = time / maxTime;

        Debug.Log("MovePerc:" + MovePerc);

        // �J�[�h�X���C�h�̈ʒu��ݒ�
        cardSlide.transform.position = new Vector3((Screen.width / 2) - (Screen.width * MovePerc), Screen.height / 2, 0);

        // �ő厞�ԂɒB���Ă��Ȃ��ꍇ�A�ēx�t�F�[�h�������s��
        if (time != maxTime && !LeftRight[3]) { StartCoroutine(CardSlideWait()); }
        else { LeftRight[2] = false; LeftRight[3] = false; }
    }

}
