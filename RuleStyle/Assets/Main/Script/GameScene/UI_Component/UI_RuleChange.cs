using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RuleChange : MonoBehaviour
{
    [SerializeField]
    private Rule_UI_RuleComponent PlayerOne;
    [SerializeField]
    private Rule_UI_RuleComponent PlayerTwo;
    [SerializeField]
    private Rule_UI_RuleComponent PlayerThree;
    [SerializeField]
    private Rule_UI_RuleComponent PlayerFour;


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


    /// <summary>
    /// �v���C���[�P�ʂ̕ύX
    /// </summary>
    /// <param name="UICompo"></param>
    void LoadUI (Rule_UI_RuleComponent UIComponent,PlayerSessionData playerdata)
    {
        GameSessionManager manager = GameSessionManager.Instance();
        //�v���C���[UI�ύX�i�������łɕύX�����̗p�ӂ�������������������Ȃ�
        UIComponent.PlayerImage.sprite = manager.card_Access["P" + playerdata.PlayerId.ToString() + "��"].cardUI;

        //�J�[�h��UI�ύX
        UIComponent.Red_Card_EffectPiece.image.sprite = playerdata.Card_Red_EffectPiece.Value.cardUI;
        UIComponent.Blue_Card.image.sprite = playerdata.Card_Blue.Value.cardUI;
        UIComponent.Red_Card_EffectAward.image.sprite = playerdata.Card_Red_EffectAward.Value.cardUI;
        UIComponent.Yellow_Card.image.sprite = playerdata.Card_Yellow.Value.cardUI;
        UIComponent.Green_Card.image.sprite = playerdata.Card_Green.Value.cardUI;
        UIComponent.Purple_Card.image.sprite = playerdata.Card_Purple.Value.cardUI;

        //���[�����ύX
        UIComponent.RuleText.text = playerdata.Rule;
    }
    /// <summary>
    /// �v���C���[�f�[�^���Q�Ƃ�UI�ɃC�x���g��t���čs�����
    /// </summary>
    public void AddOnClick(PlayerSessionData playerdata)
    {

    }
}

