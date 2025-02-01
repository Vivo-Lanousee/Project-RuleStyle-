using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorTexture; // �J�[�\���p�̃e�N�X�`��
    public Vector2 hotSpot = Vector2.zero; // �J�[�\���̃z�b�g�X�|�b�g

    void Start()
    {
        ChangeCursor();
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ChangeCursor()
    {
        if (cursorTexture != null)
        {
            // �摜�̒����㕔���z�b�g�X�|�b�g�ɐݒ�
            Vector2 hotSpot = new Vector2(cursorTexture.width / 2, 0);
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        }
        else
        {
            Debug.LogError("�J�[�\���̃e�N�X�`�����ݒ肳��Ă��܂���I");
        }
    }
}
