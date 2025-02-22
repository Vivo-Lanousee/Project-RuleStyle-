using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Rule�ύX��ʂ̃R���|�[�l���g
/// </summary>
public class ExChangeComponent : MonoBehaviour
{
    public Image CurrentPlayerImage;

    [Header("�v���C���[�̃R���|�[�l���g")]
    //Player�ׂ̍����Ή�UI��RuleComponent���ɁB
    //UI_X�̂悤�ȃi���o�[�͑Ή�����User�����Ȃ��ꍇ������������̂Ƃ���B
    #region �v���C���[�R���|�[�l���g
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

    [Header("�C�x���g�p�ϐ�")]
    public Button ExChangeEndButton;

    public Button ExChange_or_Remove_StartButton;

    #region �R�X�g_UI
    [Header("�R�X�g�p�ϐ�")]
    public Image Cost_One;
    public Image Cost_Two;
    public Image Cost_Three;
    #endregion


    /// <summary>
    /// �v���C���[�ԍ��ɕR�t����ꂽUI
    /// </summary>
    public Dictionary<int, Rule_UI_RuleComponent> Player_UI;
    /// <summary>
    /// �v���C���[�ԍ��ɕR�t����ꂽUI�̑�g
    /// </summary>
    public Dictionary<int, GameObject> UI;
    /// <summary>
    /// �R�X�gUI
    /// </summary>
    public Dictionary<int, Image> UI_Cost;

    private void Awake()
    {
        Player_UI = new Dictionary<int, Rule_UI_RuleComponent>
        {
            {1, Player_One},
            {2, Player_Two},
            {3, Player_Three},
            {4, Player_Four}
        };

        UI = new Dictionary<int, GameObject>
        {
            { 1, UI_One},
            { 2, UI_Two},
            { 3, UI_Three},
            { 4, UI_Four}
        };

        UI_Cost = new Dictionary<int, Image>
        {
            {1,Cost_One },
            {2,Cost_Two},
            {3,Cost_Three}
        };
    }
}