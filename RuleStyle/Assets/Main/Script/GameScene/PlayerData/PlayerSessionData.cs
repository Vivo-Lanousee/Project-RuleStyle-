using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Threading.Tasks;
/// <summary>
/// セッションプレイヤーデータ
/// MonoBehaviorは使わず、Playerの駒にアタッチするスクリプトは別枠で作成する
/// </summary>
public class PlayerSessionData:IDisposable
{
    /// <summary>
    /// カード全て初期化
    /// </summary>
    public void Init()
    {
        gameSessionManager = GameSessionManager.Instance();

        SubScribe();
        Reset_All();
    }

    #region カードの変数
    /// <summary>
    /// どの駒に効果が適応されるかどうか。
    /// </summary>
    public ReactiveProperty<ICard> Card_Red_EffectPiece = new ReactiveProperty<ICard>();

    /// <summary>
    /// 報酬効果対象のカード
    /// </summary>
    public ReactiveProperty<ICard> Card_Red_EffectAward = new ReactiveProperty<ICard>();

    /// <summary>
    /// 得点の条件(発生は変更時ではないので効果を
    /// Reactiveで発生するものでは無い。
    /// </summary>
    public ReactiveProperty<ICard> Card_Blue = new ReactiveProperty<ICard>();

    /// <summary>
    /// 得点で何を得るのかどうか（カードか得点か）
    /// </summary>
    public ReactiveProperty<ICard> Card_Yellow = new ReactiveProperty<ICard>();

    /// <summary>
    /// 得点の計算方法
    /// </summary>
    public ReactiveProperty<ICard> Card_Green = new ReactiveProperty<ICard>();

    /// <summary>
    ///　カードの参照する数を変更する
    /// </summary>
    public ReactiveProperty<ICard> Card_Purple = new ReactiveProperty<ICard>();
    #endregion


    /// <summary>
    /// キャラクターのカードの情報をリセットする
    /// </summary>
    public void Reset_All()
    {
        Remove_Red_EffectPiece();
        Remove_Red_EffectAward();
        Remove_Blue();
        Remove_Yellow();
        Remove_Green();
        Remove_Purple();
    }
    //Remove-色ーー特定色カードを基準カードに初期化
    #region Remove関数

    private void Remove_Red_EffectPiece()
    {
        Card_Red_EffectPiece.Value = new Card_Red_MySelf();
        Card_Red_EffectPiece.Value.Card_LoadData();
    }
    public void Remove_Red_EffectAward()
    {
        Card_Red_EffectAward.Value = new Card_Red_MySelf();
        Card_Red_EffectAward.Value.Card_LoadData();
    }
    public void Remove_Blue()
    {
        Card_Blue.Value= new Card_Blue_Goal();
        Card_Blue.Value.Card_LoadData();
    }
    public void Remove_Yellow()
    {
        Card_Yellow.Value = new Card_Yellow_Point();
        Card_Yellow.Value.Card_LoadData();
    }
    public void Remove_Green()
    {
        Card_Green.Value = new Card_Green_Plus();  
        Card_Green.Value.Card_LoadData();
    }
    public void Remove_Purple()
    {
        Card_Purple.Value = new Card_Purple_One();
        Card_Purple.Value.Card_LoadData();
    }
    #endregion

    public void Dispose()
    {   
        //判定を作った後の受け皿。
        //これらのDisposeはオブジェクトが破壊されたとしても発生させないようにする。
        ShotEvent?.Dispose();

        //ReactivePropety
        Card_Red_EffectPiece?.Dispose();
        Card_Red_EffectAward?.Dispose();
        Card_Green?.Dispose();
        Card_Yellow?.Dispose();
        Card_Green?.Dispose();
        Card_Purple?.Dispose();
    }

    /// <summary>
    /// 主にカード変更時の処理について
    /// </summary>
    public void SubScribe()
    {
       
        Card_Red_EffectPiece.Subscribe(_ => {
            EffectPiecePlayer_Id.Clear();
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                _.CardNum();

                //-------------------------------------------
                //IBlueCardに一度キャストして変換する。
                ICard_Red Red = (ICard_Red)_;
                //ここやり方が不安なんだけど問題ないのだろうか
                EffectPiecePlayer_Id = Red.EffectMember;
                //-----------------------------------------------
                Debug.Log("青(適用対象)カード変更");
            }
            
        });

        
        Card_Red_EffectAward
            .Subscribe(_ => {
                if (_ != null)
                {
                    EffectAwardPlayer_Id.Clear();
                    _.Card_PlayerChange(this);
                    _.CardNum();

                    //-------------------------------------------
                    //IBlueCardに一度キャストして変換する。
                    ICard_Red red = (ICard_Red)_;
                    //ここやり方が不安なんだけど問題ないのだろうか
                    EffectAwardPlayer_Id = red.EffectMember;
                    //-----------------------------------------------

                    Debug.Log("(報酬対象)カード変更");
                }
            });
        
        //判定カード変更なのでCardNumは行わない。UI変更のみ
        Card_Blue.Subscribe(_ => 
        {
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                Debug.Log("(ルール・判定)カード変更");
            }
        });
        //計算方法なのでCardNumは行わない。UI変更のみ
        Card_Green.Subscribe(_ =>
        {
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                Debug.Log("(計算方法の変更)カード変更");
            }
        });
        //CardNumは行わない。UI変更のみ
        Card_Yellow.Subscribe(_ => 
        {
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                Debug.Log("（得点）カード変更");
            }
        });
        Card_Purple.Subscribe(_ =>
        {
            if (_ != null)
            {
                _.Card_PlayerChange(this);
                _.CardNum();
                Debug.Log("数値）カード変更");
            }
        });

    }
    /// <summary>
    /// 自分の番のMainUISet(ルール変更はExChangeシーンでしか怒らない為
    /// メインシーンのターンプレイヤーが毎回一度呼ぶこととする
    /// </summary>
    public void UI_Set_Main()
    {
        //現在。
        gameSessionManager.Main_UI_Component.CurrentPlayerRule_Text.text = RuleText_Exchange();
        gameSessionManager.Main_UI_Component.CurrentPlayerImage.sprite = gameSessionManager.card_Access["P"+PlayerId.ToString()+"の"].cardUI;

        int relativeNum = gameSessionManager.CurrentTurnNum;
        
        for (int i = 0;i< gameSessionManager.Session_Data.Count-1; i++)
        {

            relativeNum++;
            //もし0だった場合
            if (relativeNum >= gameSessionManager.TurnList.Count)
            {
                relativeNum = 0;
            }
            switch (i)
            {
                case 0:
                    gameSessionManager.Main_UI_Component.OnePlayerRule_Text.text = gameSessionManager.Session_Data[gameSessionManager.TurnList[relativeNum]].RuleText_Exchange();
                    gameSessionManager.Main_UI_Component.OnePlayerImage.sprite = gameSessionManager.card_Access["P" + gameSessionManager.Session_Data[gameSessionManager.TurnList[relativeNum]].PlayerId + "の"].cardUI;
                    break;
                case 1:
                    gameSessionManager.Main_UI_Component.TwoPlayerRule_Text.text = gameSessionManager.Session_Data[gameSessionManager.TurnList[relativeNum]].RuleText_Exchange();
                    gameSessionManager.Main_UI_Component.TwoPlayerImage.sprite = gameSessionManager.card_Access["P" + gameSessionManager.Session_Data[gameSessionManager.TurnList[relativeNum]].PlayerId + "の"].cardUI;
                    break;
                case 2:
                    gameSessionManager.Main_UI_Component.ThreePlayerRule_Text.text = gameSessionManager.Session_Data[gameSessionManager.TurnList[relativeNum]].RuleText_Exchange();
                    gameSessionManager.Main_UI_Component.ThreePlayerImage.sprite = gameSessionManager.card_Access["P" + gameSessionManager.Session_Data[gameSessionManager.TurnList[relativeNum]].PlayerId + "の"].cardUI;
                    break;
            }
        }
    }

    /// <summary>
    /// カードの非同期ロードの事故を防ぐ為の関数
    /// メインシーンを呼ぶ時の待機処理等（普通はこちらで呼ぶ）
    /// </summary>
    /// <returns></returns>
    public async Task WaitForCardUI(Action　action)
    {
        // cardUIが設定されるまで待機
        while (gameSessionManager.card_Access["P" + PlayerId.ToString() + "の"].cardUI == null)
        {
            await Task.Yield(); // 次のフレームまで待機
        }
        action();
    }



    /// <summary>
    /// カードを誰かに変更する時の処理。「これじゃダメだった。ブルーが厳しくなる」
    /// </summary>
    public void GiveCard(ICard card,PlayerSessionData player)
    {
        switch (card.card_pattern)
        {
            
        }
    }
    /// <summary>
    /// 青のカードを誰かに付与する時の特殊関数。（UIに関わってきます。）
    /// </summary>
    public void BlueCard()
    {

    }


    /// <summary>
    /// マネージャー（UI変更等の時の処理
    /// </summary>
    public GameSessionManager gameSessionManager=null;

    /// <summary>
    /// ルールのテキスト変更
    /// </summary>
    public string RuleText_Exchange()
    {
        string text = Card_Red_EffectPiece.Value.CardName
            + Card_Blue.Value.CardName
            + Card_Red_EffectAward.Value.CardName
            + Card_Yellow.Value.CardName
            + Card_Green.Value.CardName
            + Card_Purple.Value.CardName;

        return text;
    }

    /// <summary>
    /// ルール全文
    /// </summary>
    public string Rule { get; }
    
    public int PlayerId;

    public string PlayerName=null;

    /// <summary>
    /// ショット時の判定イベント
    /// </summary>
    public IDisposable ShotEvent = null;
    /// <summary>
    /// 青のカードが発生させるショット時判定イベント
    /// </summary>
    public IDisposable BlueTrigger = null;

    /// <summary>
    /// 効果適用対象
    /// </summary>
    public List<int> EffectPiecePlayer_Id = new List<int>();

    /// <summary>
    /// 報酬対象 
    /// </summary>
    public List<int> EffectAwardPlayer_Id = new List<int>();

    /// <summary>
    /// 手札のカードリスト
    /// </summary>
    public List<ICard> HandCards = new List<ICard>();

    //個人ルール成功時の報酬量（赤カードで変更される）
    public int RuleSuccessNum = 0;

    /// <summary>
    /// プレイヤーの点数
    /// </summary>
    public int PlayerPoint=0;
    /// <summary>
    /// プレイヤーの駒(駒を作成時にアタッチする）
    /// </summary>
    public GameObject Player_GamePiece;
    //場外判定
    public bool Death=false;
    /// <summary>
    /// 個人ルール判定成功すればTrue
    /// </summary>
    public bool SuccessPoint=false;
    /// <summary>
    /// 個人ルール成功時の関数
    /// </summary>
    public void Success()
    {
        SuccessPoint = true;
    }

    /// <summary>
    /// ショット時に判定を作る時。
    /// </summary>
    public void ShotPoint()
    {
        //判定作成
        foreach (var x in gameSessionManager.Session_Data)
        {
            x.Value.Card_Blue.Value.CardNum();
            
        }

        //終了時判定を行う(動かなければ起動判定
        //ゴール時、この判定はAddToにより自動的に購読解除されるものとする。
        Player_GamePiece.transform.ObserveEveryValueChanged(x => x.position)
            .Throttle(TimeSpan.FromSeconds(1))
            .Take(1)//一回で自然にDisposeするようにする。
            .Subscribe(x =>
            { 
                Debug.Log("ショット終了");
                TurnEnd();
            })
            .AddTo(Player_GamePiece);
    }


    /// <summary>
    /// ターン終了時のあれこれ。
    /// </summary>
    public void TurnEnd()
    {
        //全員終了時の判定を確認、その後ルール成功時にルール適用する
        foreach (var y in gameSessionManager.Session_Data)
        {
            if (y.Value.SuccessPoint)
            {
                y.Value.RuleSucces();

                //全体ルールを成功していない場合要素を追加
                if (gameSessionManager.ExchangeMember.Contains(y.Key) == false)
                {
                    gameSessionManager.ExchangeMember.AddLast(y.Key);
                }

                y.Value.SuccessPoint = false;
            }
        } 

        //ターン終了時、必ず一度改変モードに移行する。
        gameSessionManager.sceneContext.Mode_Change(new GameMode_ExchangeMode());
    }

    /// <summary>
    /// 個人ルール成功時のリワード。
    /// </summary>
    public void RuleSucces()
    {
        //個人ルール達成時のリワード
        Card_Yellow.Value.CardNum();

        
    }

    /// <summary>
    /// ゴール成功時のリワード
    /// （ゴール側に「衝突した場合」のスクリプトを記載し
    /// 直接ゴール時の処理が走ります
    /// </summary>
    public void GoalReward()
    {
        gameSessionManager.DeckDraw(this, 2);

        //個人ルールを成功していない場合、ルール変更の処理を行う為のLinkedListに格納。
        if (gameSessionManager.ExchangeMember.Contains(this.PlayerId) == false)
        {
            gameSessionManager.ExchangeMember.AddLast(this.PlayerId);
        }

        //ターン終了
        TurnEnd() ;
        //改変モードに移行する。
    }
    
    /// <summary>
    /// 駒が盤面上に存在しない場合のスクリプト
    /// </summary>
    /// <param name="MyPiece"></param>
    public void PlayerPieceCreate()
    {
        gameSessionManager = GameSessionManager.Instance();

        switch (PlayerId) 
        {
        case 1:
                Player_GamePiece =UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_One,gameSessionManager.PieceStartPoint,Quaternion.identity);
                break;
        case 2:
                Player_GamePiece = UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_Two, gameSessionManager.PieceStartPoint, Quaternion.identity);
                break;
        case 3:
                Player_GamePiece = UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_Three, gameSessionManager.PieceStartPoint, Quaternion.identity);
                break;
        case 4:
                Player_GamePiece = UnityEngine.Object.Instantiate(gameSessionManager.PlayerGameObject_Four, gameSessionManager.PieceStartPoint, Quaternion.identity);
                break;
        }
        Death = false;

        /*
        //生成時、全体ルールを適応した場合。
        Player_GamePiece.transform.UpdateAsObservable()
            .Subscribe(x =>
            {

            }).AddTo(Player_GamePiece);*/
    }
}