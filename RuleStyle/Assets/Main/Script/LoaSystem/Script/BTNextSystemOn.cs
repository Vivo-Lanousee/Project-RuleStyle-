using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BTNextSystemOn : MonoBehaviour
{
    [SerializeField]
    bool first = false;

    [SerializeField]
    GameObject next;
    [SerializeField]
    Button myBT;

    [SerializeField]
    bool myBTActiveChange = true;

    [SerializeField]
    Button returnBT;

    GameObject myCanvas;

    BitArray extraCompletion = new BitArray(1, false);
    BitArray All = new BitArray(1, true);
    private void Awake()
    {
        myCanvas = GetComponent<GameObject>();
        if (first)
        {
            SystemOn();
        }
    }

    public void SystemOn()
    {
        myBT.onClick.AddListener(OnNextSystem);

        if (returnBT)
        {
            returnBT.onClick.AddListener(ReturnSystem);
        }
    }

    void OnNextSystem()
    {
        StartCoroutine(StartSEWait());
        StartCoroutine(WaitNExt());

    }

    IEnumerator WaitNExt()
    {
        yield return new WaitUntil(() => All.Cast<bool>().SequenceEqual(extraCompletion.Cast<bool>()));
        extraCompletion[0] = false;
        next.SetActive(true);

        BTNextSystemOn nextSystem = next.GetComponent<BTNextSystemOn>(); ;

        if (nextSystem) { nextSystem.SystemOn(); }

        myBT.onClick.RemoveListener(OnNextSystem);

        if (returnBT)
        {
            returnBT.onClick.AddListener(ReturnSystem);
        }

        if (myBTActiveChange)
        {
            myBT.transform.parent.gameObject.SetActive(false);
        }

    }

    void ReturnSystem()
    {
        AudioManager audioManager = AudioManager.Instance();

        audioManager.Play(AudioType.NextSE);

        next.SetActive(false);
        myBT.onClick.AddListener(OnNextSystem);

        if (returnBT)
        {
            returnBT.onClick.RemoveListener(ReturnSystem);
        }

        if (myBTActiveChange)
        {
            myBT.transform.parent.gameObject.SetActive(true);
        }
    }

    IEnumerator StartSEWait()
    {
        AudioManager audioManager = AudioManager.Instance();

        yield return new WaitUntil(() => (audioManager != null) ? true : false);

        audioManager.PlaySE();
        extraCompletion[0] = true;
    }
}
