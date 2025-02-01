using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��]�������`����񋓌^ (Enum to define rotation directions)
public enum RotationDis
{
    right = 1,  // �E��] (Right rotation)
    left = -1,  // ����] (Left rotation)
    none = 0    // ��]�Ȃ� (No rotation)
}

public class RotationUI : MonoBehaviour
{
    // ��]����UI�v�f�iRectTransform�j�̃��X�g (List of RectTransforms for rotating UI elements)
    List<RectTransform> rotationUI = new List<RectTransform>();

    Vector2 dspsize = new Vector2(Screen.width, Screen.height); // ��ʃT�C�Y (Screen size)
    Vector2 percent = new Vector2(0.9f, 0.7f); // UI����߂銄���i0�`1�j (The percentage of screen the UI takes)

    [SerializeField, Header("�����܂ł̎���")]
    float timeToComplete = 2; // ��]����������܂ł̎��� (Time to complete the rotation)

    RotationDis rotationDis = RotationDis.right; // ��]�����i�E��]���f�t�H���g�j (Default direction is right)

    // �e�����̊p�x�f�[�^�i180�x�A90�x�A0�x�A270�x�j (Angles for each direction)
    float[] DirectionsData = new float[4] { 180, 90, 0, 270 };
    List<float> Directions = new List<float>(); // ���ۂɎg�p��������f�[�^�̃��X�g (List for actual directions used)

    int playerNumber = 0; // �v���C���[�� (Number of players)

    // ��ԊǗ��p��BitArray (State management using BitArray)
    // 0 = �l���擾���� / 1 = rotationUI�擾���� / 2 = ��]���s��ԊǗ� (Flags for different stages)
    BitArray bit = new BitArray(4, false);

    private void Awake()
    {
        StartCoroutine(SetDirections()); // ��]�ɕK�v�ȏ���ݒ� (Set up rotation directions)
    }

    /// <summary>
    /// UI��RectTransform�������郁�\�b�h (Method to assign RectTransform for UI elements)
    /// </summary>
    /// <param name="inRotationUI">�ǉ�����RectTransform (RectTransform to add)</param>
    /// <param name="num">�v���C���[�ԍ��i1����n�܂�j(Player number starting from 1)</param>
    public void InRotationUI(RectTransform inRotationUI, int num)
    {
        StartCoroutine(InRotationUIWait(inRotationUI, num)); // �v���C���[UI�̒ǉ���ҋ@ (Wait for player UI to be added)
    }

    /// <summary>
    /// �l�����m�肷��܂őҋ@����R���[�`�� (Coroutine to wait until the number of players is confirmed)
    /// </summary>
    /// <param name="inRotationUI">�ǉ�����RectTransform (RectTransform to add)</param>
    /// <param name="num">�v���C���[�ԍ� (Player number)</param>
    /// <returns></returns>
    IEnumerator InRotationUIWait(RectTransform inRotationUI, int num)
    {
        // �l�����m�肷��܂őҋ@ (Wait until the number of players is set)
        yield return new WaitUntil(() => bit[0]);
        if (num <= rotationUI.Count)
        {
            rotationUI[num - 1] = inRotationUI; // UI��RectTransform�����X�g�ɒǉ� (Add RectTransform to list)
            playerNumber++; // �v���C���[�����X�V (Update player count)
        }
    }

    /// <summary>
    /// �K�v�ȏ�񂪑����܂őҋ@���Đݒ���s���R���[�`�� (Coroutine to wait for required info and then set up)
    /// </summary>
    /// <returns></returns>
    IEnumerator SetDirections()
    {
        GameManager gameManager = GameManager.Instance(); // �Q�[���}�l�[�W���̃C���X�^���X���擾 (Get GameManager instance)
        yield return new WaitUntil(() => gameManager); // �Q�[���}�l�[�W���������ł���܂őҋ@ (Wait for GameManager to be ready)

        // �v���C���[���ɍ��킹��UI���X�g��ݒ� (Set up UI list based on the number of players)
        rotationUI = new List<RectTransform>(new RectTransform[gameManager.PlayerNum]);
        bit[0] = true; // �l���ݒ芮�� (Number of players set)
        Debug.Log("RotationUI �l���ݒ芮�� /num:" + rotationUI.Count);

        // ��]�����̐ݒ� (Set rotation directions based on number of players)
        switch (rotationUI.Count)
        {
            case 4:
                DirectionsData = ((rotationDis == RotationDis.right) ? new float[4] { 180, 90, 0, 270 } : new float[4] { 180, 270, 0, 90 });
                break;
            case 3:
                DirectionsData = ((rotationDis == RotationDis.right) ? new float[3] { 180, 90, 270 } : new float[3] { 180, 270, 90 });
                break;
            case 2:
                DirectionsData = new float[2] { 180, 0 }; // 2�l�p (For 2 players)
                break;
        }

        // UI����RectTransform��l�����󂯎��܂őҋ@ (Wait until RectTransforms for all players are received)
        yield return new WaitUntil(() => (rotationUI.Count == playerNumber));
        Debug.Log("RotationUI RectTransform �󂯎�芮��");


        RotationSet(0,0); // ��]�ݒ� (Set up rotation)
        bit[1] = true; // UI�ݒ芮�� (UI set)
    }

    /// <summary>
    /// ��]���J�n���郁�\�b�h (Method to start rotation)
    /// </summary>
    public void StartRotation()
    {
        if (!bit[2]) { StartCoroutine(StartRotationWait()); } // ��]�������s�Ȃ�J�n (Start rotation if it hasn't been executed)
    }

    /// <summary>
    /// ��]�J�n��ҋ@����R���[�`�� (Coroutine to wait for rotation to start)
    /// </summary>
    /// <returns></returns>
    IEnumerator StartRotationWait()
    {
        bit[2] = true; // ��]���s��Ԃɐݒ� (Set to rotation in progress)

        yield return new WaitUntil(() => bit[1]); // UI�ݒ芮����ҋ@ (Wait for UI setup to complete)

        Debug.Log("RotationUI ��]�J�n");

        Time_TimerManager time_TimerManager = Time_TimerManager.Instance();
        time_TimerManager.Fade(OnRotation, timeToComplete); // �t�F�[�h���ʂŉ�]���J�n (Start rotation with fade effect)
    }

    // ��]���s�� (Perform the rotation)
    void OnRotation(float perc)
    {
        // ��]�p�x���v�Z (Calculate the rotation angle)
        float dis = ((Mathf.PI * 0.5f) * perc) * ((rotationDis == RotationDis.right) ? 1 : -1);

        RotationSet(dis, perc); // ��]�𔽉f (Apply the rotation)
        if (perc == 1)
        {
            bit[2] = false; // ��]�����t���O (Set flag to indicate rotation is complete)
            ResetRotationUI(); // ��]�I����AUI�����Z�b�g (Reset the UI after rotation)
        }
    }

    /// <summary>
    /// UI�̉�]�����Z�b�g���� (Reset the UI rotation)
    /// </summary>
    public void ResetRotationUI()
    {
        // ���X�g����UI��1�����ɃV�t�g (Shift UI elements in the list to the left)
        RectTransform zerorect = rotationUI[0];
        for (int i = 0; i < rotationUI.Count - 1; i++)
        {
            rotationUI[i] = rotationUI[i + 1]; // �ړ� (Shift the UI elements)
        }

        // �Ō�ɍŏ���UI���ړ� (Move the first UI element to the end)
        rotationUI[rotationUI.Count - 1] = zerorect;
    }

    /// <summary>
    /// UI�̉�]��ݒ肷�� (Set the rotation for the UI elements)
    /// </summary>
    /// <param name="dis">��]�ʁi���W�A���j (Rotation amount in radians)</param>
    void RotationSet(float dis, float perc)
    {
        Debug.Log("RotationSet �X�^�[�g");
        for (int i = 0; i < rotationUI.Count; i++)
        {
            float xdis = dis;
            switch (rotationUI.Count)
            {
                case 3:
                    xdis *= (i == 2) ? 2 : 1; // 3�l�̂Ƃ��A�Ō��UI�̉�]�ʂ�ύX (For 3 players, adjust the rotation for the last UI element)
                    break;
                case 2:
                    xdis *= 2; // 2�l�̂Ƃ��A��]�ʂ�ύX (For 2 players, adjust the rotation amount)
                    break;
            }

            // ��]��̈ʒu���v�Z (Calculate the new position after rotation)
            Vector2 vec = new Vector2(Mathf.Sin((DirectionsData[i] * Mathf.Deg2Rad) + xdis), Mathf.Cos((DirectionsData[i] * Mathf.Deg2Rad) + xdis));

            if (i == 0)
            {
                // ������ړ�
                // ��������
                vec *= (dspsize / 2) * new Vector2(percent.x * (Mathf.Abs(perc - 1) + 1f), 1 - (perc * Mathf.Abs(percent.y-1)));
                // ��_����
                vec += dspsize * new Vector2((Mathf.Abs(perc) * 0.45f) + 0.05f, 0.58f);
            }
            else if (i == 1)
            {
                // ���Ɉړ�����
                // ��������
                vec *= (dspsize / 2) * new Vector2(percent.x * (Mathf.Abs(perc) + 1f), 1 - (Mathf.Abs(perc-1) * Mathf.Abs(percent.y - 1)));
                // ��_����
                vec += dspsize * new Vector2((Mathf.Abs(perc-1) * 0.45f) + 0.05f, 0.58f);
            }
            else
            {
                // ��������
                vec *= (dspsize / 2) * percent;
                // ��_����
                vec += dspsize * new Vector2(0.5f, 0.58f);
            }
            rotationUI[i].position = vec; // �v�Z�����ʒu��UI���ړ� (Move UI to the calculated position)
        }
    }
}
