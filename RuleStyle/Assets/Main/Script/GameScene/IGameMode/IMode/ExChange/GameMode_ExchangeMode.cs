using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// カード変更シーンの設定。
/// </summary>
public class GameMode_ExchangeMode : IGameMode
{
    private GameSessionManager sessionManager;

    ExChangeComponent ExChange;

    /// <summary>
    /// 現在の変更プレイヤ-を格納する。
    /// </summary>
    private PlayerSessionData Change_CurrentPlayer;

    /// <summary>
    /// カードの情報とイベント付けを全て行う。
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    void IGameMode.Init()
    {
        sessionManager = GameSessionManager.Instance();
        if (sessionManager.ExchangeMember.Count ==0)
        {
            sessionManager.sceneContext.Mode_Change(new GameMode_TurnChange(sessionManager));
            Debug.Log("もう改変ができるメンバーはいません。");
        }
        else if(sessionManager.ExchangeMember.Count > 0)
        {
            ExChange=sessionManager.ChangeScene.GetComponent<ExChangeComponent>();
            Debug.Log("改変できるメンバーが存在します。");

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
    /// プレイヤー単位の変更
    /// </summary>
    /// <param name="UICompo"></param>
    void LoadUI(Rule_UI_RuleComponent UIComponent, PlayerSessionData playerdata)
    {
        GameSessionManager manager = GameSessionManager.Instance();
        //プレイヤーUI変更（もうすでに変更したの用意した方がいいかもしれない
        UIComponent.PlayerImage.sprite = manager.card_Access["P" + playerdata.PlayerId.ToString() + "の"].cardUI;

        //カードのUI変更(セッションデータで既にロードされているデータの為、基本的にカードはアタッチするだけで問題ない
        UIComponent.Red_Card_EffectPiece.image.sprite = playerdata.Card_Red_EffectPiece.Value.cardUI;
        UIComponent.Blue_Card.image.sprite = playerdata.Card_Blue.Value.cardUI;
        UIComponent.Red_Card_EffectAward.image.sprite = playerdata.Card_Red_EffectAward.Value.cardUI;
        UIComponent.Yellow_Card.image.sprite = playerdata.Card_Yellow.Value.cardUI;
        UIComponent.Green_Card.image.sprite = playerdata.Card_Green.Value.cardUI;
        UIComponent.Purple_Card.image.sprite = playerdata.Card_Purple.Value.cardUI;

        //ルール文変更
        UIComponent.RuleText.text = playerdata.Rule;
    }
    /// <summary>
    /// プレイヤーデータを参照にUIにイベントを付けて行く作業
    /// </summary>
    public void AddOnClick(PlayerSessionData playerdata)
    {
        ExChange.ExChangeEndButton.onClick.AddListener(() => 
        {
            //再度変更モードを読み込むことで全て消化する。
            sessionManager.sceneContext.Mode_Change(new GameMode_ExchangeMode());


            sessionManager.ChangeScene.SetActive(false);
        });
    }
}
