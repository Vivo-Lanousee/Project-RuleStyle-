using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardChangeBT : MonoBehaviour
{
    [SerializeField]
    Button[] buttons = new Button[0];

    private void Awake()
    {
        //play�l��
        int num = 4;
        for (int i = num; i < 4; i++)
        {
            RectTransform rect = buttons[i].transform.gameObject.GetComponent<RectTransform>();
            rect.position = new Vector2(Screen.width, Screen.height) * 2;
        }

        OnClick(0);
    }

    void AllActiveChange(bool c)
    {
        foreach (var button in buttons) { button.interactable = c; }
    }

    void OnClick(int s)
    {
        Debug.Log(s);

        // �z��͈̔̓`�F�b�N
        if (s < 0 || s >= buttons.Length)
        {
            Debug.LogError("Invalid index: " + s);
            return; // �͈͊O�Ȃ珈���𒆎~
        }

        AllActiveChange(true);

        buttons[s].onClick.RemoveListener(() => OnClick(s));
        buttons[s].interactable = false;

        // i �̌��݂̒l���L���v�`�����Ȃ��悤�ɁA�ʂ̕ϐ����g��
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // �ꎞ�I�ɕϐ��Ɋi�[
            if (index != s)
            {
                Debug.Log(index);
                // s �ȊO�̃{�^���Ƀ��X�i�[��ǉ�
                buttons[index].onClick.AddListener(() => OnClick(index)); // ������ index ���g��
            }
        }
    }


}
