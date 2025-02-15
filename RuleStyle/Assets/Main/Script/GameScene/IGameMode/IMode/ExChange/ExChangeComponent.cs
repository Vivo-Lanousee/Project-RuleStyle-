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

    [Header("�R�X�g�p�ϐ�")]
    public Image Cost_One;
    public Image Cost_Two;
    public Image Cost_Three;
}