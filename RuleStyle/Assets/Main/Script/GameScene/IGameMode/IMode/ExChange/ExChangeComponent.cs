using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Rule変更画面のコンポーネント
/// </summary>
public class ExChangeComponent : MonoBehaviour
{
    //Playerの細かい対応UIはRuleComponent内に。
    //UI_Xのようなナンバーは対応するUserがいない場合消失させるものとする。
    #region プレイヤーコンポーネント
    public GameObject UI_One;
    public Rule_UI_RuleComponent Player_One;
    public GameObject UI_Two;
    public Rule_UI_RuleComponent Player_Two;
    public GameObject UI_Three;
    public Rule_UI_RuleComponent Player_Three;
    public GameObject UI_Four;
    public Rule_UI_RuleComponent Player_Four;
    #endregion

    //

    [Header("イベント用変数")]
    public Button ExChangeEndButton;
}