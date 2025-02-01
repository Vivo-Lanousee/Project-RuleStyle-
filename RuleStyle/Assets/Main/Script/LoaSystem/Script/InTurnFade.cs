using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InTurnFade : MonoBehaviour
{
    [SerializeField]
    RectMask2D[] rctMasks; // ������RectMask2D�R���|�[�l���g��ێ�����z��

    Vector2 dspsize = new Vector2(Screen.width, Screen.height); // ��ʃT�C�Y
    float maxTime = 0.05f; // �t�F�[�h�̍ő厞��
    float waitTime = 0.01f; // �t�F�[�h���̑ҋ@����

    int num = 3; // �t�F�[�h����Mask�̃C���f�b�N�X
    bool conp = false; // �t�F�[�h�����t���O

    private void Awake()
    {
        MaskReset(); // Mask��������
        GameManager gameManager = GameManager.Instance(); // �Q�[���}�l�[�W���̃C���X�^���X���擾
        StartCoroutine(FadeNumWait(gameManager.PlayerNum - 1)); // �v���C���[�̐��ɉ������t�F�[�h�J�n
    }

    /// <summary>
    /// RectMask2D��padding�����Z�b�g����
    /// </summary>
    public void MaskReset()
    {
        // ���ׂĂ�RectMask2D��padding����ʊO�ɐݒ�
        for (int i = 0; i < rctMasks.Length; i++)
        {
            rctMasks[i].padding = new Vector4(0, (dspsize.y + 300), 0, -300); // �㉺�ɉ�ʃT�C�Y + 300�̗]����ǉ�
        }
    }

    /// <summary>
    /// �t�F�[�h�����Ԃɑҋ@���ď�������R���[�`��
    /// </summary>
    /// <param name="i">���݂̃v���C���[�C���f�b�N�X</param>
    IEnumerator FadeNumWait(int i)
    {
        yield return new WaitForSeconds(0.5f); // �����ҋ@�i0.5�b�j
        num = i; // �v���C���[���Ɋ�Â��ăC���f�b�N�X��ݒ�
        conp = false; // �t�F�[�h�����t���O�����Z�b�g
        Time_TimerManager time_TimerManager = Time_TimerManager.Instance(); // ���ԊǗ��}�l�[�W�����擾

        // �t�F�[�h�������Ăяo���A������ҋ@
        time_TimerManager.Fade(FadeWait, maxTime, FadeSpecified._1to0);

        // �t�F�[�h����������܂őҋ@
        yield return new WaitUntil(() => conp);

        // �v���C���[����1���傫����΁A���̃C���f�b�N�X�ōēx�t�F�[�h�����s
        if (i > 0) { StartCoroutine(FadeNumWait((i - 1))); }
    }

    /// <summary>
    /// �t�F�[�h���ɌĂяo�����R�[���o�b�N�֐�
    /// </summary>
    /// <param name="perc">�t�F�[�h�i�s�󋵁i0�`1�j</param>
    void FadeWait(float perc)
    {
        // RectMask2D��padding��i�s�󋵂ɉ����ĕύX�i�t�F�[�h��\���j
        rctMasks[num].padding = new Vector4(0, (dspsize.y + 300) * perc, 0, -300);

        // �t�F�[�h���I�������ꍇ�A�����t���O��true�ɐݒ�
        if (perc == 0) { conp = true; }
    }
}
