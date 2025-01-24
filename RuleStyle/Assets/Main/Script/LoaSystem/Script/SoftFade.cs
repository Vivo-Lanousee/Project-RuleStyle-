using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoftFade : MonoBehaviour
{
    Button my;
    [SerializeField]
    RectMask2D mask;

    [SerializeField, Header("Š®—¹‚Ü‚Å‚ÌŽžŠÔ")]
    float timeToComplete = 2;
    float timer = 0;

    private void Awake()
    {
        my = GetComponent<Button>();
        Debug.Log("/x:" + mask.padding.x+"/y:" + mask.padding.y + "/z:" + mask.padding.z + "/z:" + mask.padding.w);
    }

    void Start()
    {
        
    }

    public void MaskReset()
    {
        mask.padding = new Vector4(0,900,0,-300);
    }

}
