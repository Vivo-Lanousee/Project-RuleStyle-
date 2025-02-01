using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorChanger : MonoBehaviour
{
    [SerializeField, Header("�J�[�\����ύX����")]
    // �J�[�\���p�̃e�N�X�`��
    Texture2D cursorTexture;


    // �_�Ŏ��̃J�[�\���摜
    Texture2D blinkTexture;


    // �_�ł̊Ԋu�i�b�j
    float blinkInterval = 0.3f;


    // �J�[�\���̃z�b�g�X�|�b�g click����ʒu
    // �f�t�H���g�͍���
    Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        // �摜�̒����㕔���z�b�g�X�|�b�g�ɐݒ�
        hotSpot = new Vector2(cursorTexture.width / 2, 0);

        // �J�[�\���̓_�ŗp�ɃA���t�@�l�𒲐������摜���쐬
        blinkTexture = ChangeTextureAlpha(cursorTexture, 0.5f);

        // �����J�[�\����ݒ�
        ChangeCursor();

        StartCoroutine(BlinkCursor());
    }

    /// <summary>
    /// �J�[�\���摜�̃A���t�@�l��ύX����
    /// </summary>
    /// <param name="texture">���̃e�N�X�`��</param>
    /// <param name="alpha">�ݒ肷��A���t�@�l�i0.0�`1.0�j</param>
    /// <returns>�A���t�@�l��ύX�����V�����e�N�X�`��</returns>
    Texture2D ChangeTextureAlpha(Texture2D texture, float alpha)
    {
        // �V�����e�N�X�`�����쐬
        Texture2D newTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);

        // �S�s�N�Z���̐F���擾
        Color[] pixels = texture.GetPixels();

        // �e�s�N�Z���̃A���t�@�l��ύX
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i].a *= alpha;
        }

        // �V�����e�N�X�`���ɕύX��K�p
        newTexture.SetPixels(pixels);
        newTexture.Apply();

        return newTexture;
    }

    /// <summary>
    /// �J�[�\����ύX����
    /// </summary>
    public void ChangeCursor()
    {
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        }
        else
        {
            Debug.LogError("�J�[�\���̃e�N�X�`�����ݒ肳��Ă��܂���I");
        }
    }

    /// <summary>
    /// �J�[�\����_�ł�����R���[�`��
    /// </summary>
    IEnumerator BlinkCursor()
    {
        // �ʏ�J�[�\��
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);

        // �{�^��object�ɃJ�[�\�����d�Ȃ��Ă��邩�ǂ���
        // �m�F����֐����Ăяo���ē_�ł��J�n���邩�m�F���Ă���
        yield return new WaitUntil(() => IsCursorOverButton());

        // �ҋ@����
        yield return new WaitForSeconds(blinkInterval);

        // �_�ŃJ�[�\���i�����x�����������́j
        Cursor.SetCursor(blinkTexture, hotSpot, CursorMode.Auto);
        yield return new WaitForSeconds(blinkInterval);


        // �ċA
        StartCoroutine(BlinkCursor());
        
    }

    /// <summary>
    /// �J�[�\�����w�肵�� UI �I�u�W�F�N�g��ɂ��邩�m�F����
    /// ����̓{�^���ɏd�Ȃ��Ă��邩�m�F��
    /// </summary>
    /// <param name="uiObject">�`�F�b�N���� UI �� GameObject</param>
    /// <returns>�J�[�\���� UI ��ɂ���ꍇ�� true</returns>
    bool IsCursorOverButton()
    {
        // �}�E�X�̌��݂̈ʒu���擾
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // UI �� Raycast ���擾
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // �J�[�\���� Button ������ UI �ɏd�Ȃ��Ă��邩�m�F
        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null) // Button�R���|�[�l���g�������Ă��邩
            {
                return true;
            }
        }
        return false;
    }

}
