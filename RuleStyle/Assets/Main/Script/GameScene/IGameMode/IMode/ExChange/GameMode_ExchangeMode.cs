using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// �J�[�h�ύX�V�[���̐ݒ�B
/// </summary>
public class GameMode_ExchangeMode : IGameMode
{
    private GameSessionManager sessionManager;

    ExChangeComponent ExChange;

    /// <summary>
    /// ���݂̕ύX�v���C��-���i�[����B
    /// </summary>
    private PlayerSessionData Change_CurrentPlayer;

    /// <summary>
    /// �J�[�h�̏��ƃC�x���g�t����S�čs���B
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    void IGameMode.Init()
    {
        sessionManager = GameSessionManager.Instance();
        if (sessionManager.ExchangeMember.Count ==0)
        {
            sessionManager.sceneContext.Mode_Change(new GameMode_TurnChange(sessionManager));
            Debug.Log("�������ς��ł��郁���o�[�͂��܂���B");
        }
        else if(sessionManager.ExchangeMember.Count > 0)
        {
            ExChange=sessionManager.ChangeScene.GetComponent<ExChangeComponent>();
            Debug.Log("���ςł��郁���o�[�����݂��܂��B");

            int current = sessionManager.ExchangeMember.First.Value;
            sessionManager.ExchangeMember.RemoveFirst();
            Change_CurrentPlayer = sessionManager.Session_Data[current];

            sessionManager.ChangeScene.SetActive(true);

            AddOnClick(Change_CurrentPlayer);


        }
    }
    void IGameMode.Exit()
    {
    }

    void IGameMode.FixUpdate()
    {
    }

    

    void IGameMode.Update()
    {
    }
    /// <summary>
    /// �v���C���[�P�ʂ̕ύX
    /// </summary>
    /// <param name="UICompo"></param>
    void LoadUI(Rule_UI_RuleComponent UIComponent, PlayerSessionData playerdata)
    {
        GameSessionManager manager = GameSessionManager.Instance();
        //�v���C���[UI�ύX�i�������łɕύX�����̗p�ӂ�������������������Ȃ�
        UIComponent.PlayerImage.sprite = manager.card_Access["P" + playerdata.PlayerId.ToString() + "��"].cardUI;

        //�J�[�h��UI�ύX(�Z�b�V�����f�[�^�Ŋ��Ƀ��[�h����Ă���f�[�^�ׁ̈A��{�I�ɃJ�[�h�̓A�^�b�`���邾���Ŗ��Ȃ�
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
        ExChange.ExChangeEndButton.onClick.AddListener(() => 
        {
            //�ēx�ύX���[�h��ǂݍ��ނ��ƂőS�ď�������B
            sessionManager.sceneContext.Mode_Change(new GameMode_ExchangeMode());


            sessionManager.ChangeScene.SetActive(false);
        });
    }
}
