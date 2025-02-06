using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RuleChange : MonoBehaviour
{
    [SerializeField]
    private UI_RuleComponent PlayerOne;
    [SerializeField]
    private UI_RuleComponent PlayerTwo;
    [SerializeField]
    private UI_RuleComponent PlayerThree;
    [SerializeField]
    private UI_RuleComponent PlayerFour;


    public void UIChange()
    {
        GameSessionManager manager=GameSessionManager.Instance();

        foreach(var userdata in manager.Session_Data)
        {
            switch (userdata.Key)
            {
                case 1:
                    
                    //PlayerOne.Red_Card_EffectAward=userdata.Value.Card_Blue_EffectAward;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
    }
    void LoadUI ()
    {
       
    }
    /// <summary>
    /// �v���C���[�f�[�^���Q�Ƃ�UI�ɃC�x���g��t���čs�����
    /// </summary>
    public void AddOnClick(PlayerSessionData playerdata)
    {

    }
}

/// <summary>
/// Rule�ύX��ʂ�UI
/// </summary>
[Serializable]
public class UI_RuleComponent
{
    public Button PlayerImage;
    public Button Red_Card_EffectPiece;
    public Button Blue_Card;
    public Button Red_Card_EffectAward;
    public Button Yellow_Card;
    public Button Green_Card;
    public Button Purple_Card;


    public TMPro.TextMeshPro RuleText;
}
