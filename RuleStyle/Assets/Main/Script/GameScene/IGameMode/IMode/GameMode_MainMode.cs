using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameMode_MainMode : IGameMode
{
    GameSessionManager GameSceneManager;

    PlayerSessionData player;

    #region �J�����R���|�[�l���g
    /// <summary>
    /// ���S
    /// </summary>
    public Vector3 Cont = new Vector3(0, 0, 0);
    public float Dist = 3f;
    #endregion

    public ReactiveProperty<bool> dragged = new ReactiveProperty<bool>(false);

    private Vector3 position;
    private Rigidbody rb;
    private Vector3 dragOffset;

    public GameMode_MainMode(GameSessionManager gameSceneManager)
    {
        GameSceneManager = gameSceneManager;
    }

    /// <summary>
    /// �ύX���鐔�l
    /// </summary>
    public float ChangeMeter = 0;

    /// <summary>
    /// �����Ŏ��̃v���C���[�̏�������
    /// </summary>
    void IGameMode.Init()
    {
        player=GameSceneManager.NowPlayer();

        //�Ֆʏ�ɑ��݂��Ȃ��ꍇ�B
        if (player.Player_GamePiece==null)
        {
            player.PlayerPieceCreate();
        }

        //���݂̃L�����N�^�[�̒n�_
        Cont = player.Player_GamePiece.transform.position;
        Cont.y = 0;


        rb=player.Player_GamePiece.GetComponent<Rigidbody>();
        dragged.Where(_ => _ == true).Subscribe(_ => {
            position = Input.mousePosition;
        }).AddTo(player.Player_GamePiece);
    }

    /// <summary>
    /// �V���b�g�����쐬����B
    /// </summary>
    void IGameMode.Update()
    {
        CameraRole();

        Line();
    }

    void CameraRole()
    {
        // ����l�ɍs���ΐ��l��߂�
        if (ChangeMeter >= 361)
        {
            ChangeMeter = 0;
        }
        if (ChangeMeter < 0)
        {
            ChangeMeter = 360;
        }

        // ���l��ύX
        if (Input.GetMouseButton(2))
        {
            ChangeMeter += 0.3f;
        }
        if (Input.GetMouseButton(1))
        {
            ChangeMeter -= 0.3f;
        }

        //���݂̃L�����N�^�[�̒n�_
        Cont = player.Player_GamePiece.transform.position;
        Cont.y = 0;

        // ���W�A���ϊ�
        float test = ChangeMeter * Mathf.Deg2Rad;
        // �ړ�����ׂ��n�_���Z�o
        float b = Mathf.Cos(test);
        float a = Mathf.Sin(test);
        // �n�_�쐬
        Vector3 t = new Vector3(b * Dist, 3f, a * Dist);
        // �J�����̈ʒu��ύX
        GameSceneManager.CameraPosition.transform.position = t + Cont;

        // �v���C���[�̕�������������
        GameSceneManager.CameraPosition.transform.LookAt(player.Player_GamePiece.transform);

        // �����X���p�x��ݒ�i�Ⴆ��30�x�j
        float fixedXRotation = 30f;

        // ���݂̉�]���擾
        Vector3 fixedRotation = GameSceneManager.CameraPosition.transform.rotation.eulerAngles;

        // X����]���Œ�i����̒l�ɐݒ�j
        fixedRotation.x = fixedXRotation;

        // Y����Z���̉�]���ێ����AX�������X�V
        GameSceneManager.CameraPosition.transform.rotation = Quaternion.Euler(fixedRotation);
    }
    /*
    public void Line()
    {
        Vector3 cameraoffset = player.Player_GamePiece.transform.position - GameSceneManager.CameraPosition.transform.position;
        if (Input.GetMouseButton(0))
        {
            dragged.Value = true;

            //�h���b�O�J�n����̍��l�̃x�N�g��
            Vector3 direction = Input.mousePosition - position;
            

            //���W�A���p�x���o
            var rad = Mathf.Atan2(direction.y, direction.x);
            Debug.Log(rad);

            float x = Mathf.Cos(rad);
            float z = Mathf.Sin(rad);

            //�x�N�g���쐬
            dragOffset = new Vector3(x, 0, z);

            
            //������10�ȍ~�������ꍇ
            if (direction.magnitude > 10)
            {
                Debug.DrawLine(player.Player_GamePiece.transform.position, new Vector3(dragOffset.x * 10, 1, dragOffset.z * 10), Color.black);
            }
            //������10�ȑO�������ꍇ
            else
            {
                Debug.DrawLine(player.Player_GamePiece.transform.position, new Vector3(dragOffset.x * direction.magnitude, 1, dragOffset.z * direction.magnitude), Color.black);
            }

        }
        //��������
        else if (dragged.Value == true)
        {
            dragged.Value = false;

            rb.AddForce(dragOffset * 27, ForceMode.Impulse);
        }
    }*/
    public void Line()
    {
        Vector3 cameraOffset = player.Player_GamePiece.transform.position - GameSceneManager.CameraPosition.transform.position;

        if (Input.GetMouseButton(0))
        {
            dragged.Value = true;

            // �h���b�O�J�n����̍����̃x�N�g��
            Vector3 direction = Input.mousePosition - position;

            // ���W�A���p�x���v�Z
            var rad = Mathf.Atan2(direction.y, direction.x);
            Debug.Log(rad);

            // �J�����̕�������ɂ����x�N�g�����쐬
            // �J�����̑O�����ƉE�������擾
            Vector3 cameraForward = GameSceneManager.CameraPosition.transform.forward;
            cameraForward.y = 0; // ���������͖���
            cameraForward.Normalize(); // ���K��

            Vector3 cameraRight = GameSceneManager.CameraPosition.transform.right;
            cameraRight.y = 0; // ���������͖���
            cameraRight.Normalize(); // ���K��

            // �J�����̑O���ƉE��������Ƀh���b�O������ݒ�
            Vector3 dragDirection = cameraForward * Mathf.Sin(rad) + cameraRight * Mathf.Cos(rad);

            // �x�N�g���쐬
            dragOffset = dragDirection;

            // ������10�ȍ~�������ꍇ
            if (direction.magnitude > 10)
            {
                Debug.DrawLine(player.Player_GamePiece.transform.position, new Vector3(dragOffset.x * 10, 1, dragOffset.z * 10), Color.black);
            }
            // ������10�ȑO�������ꍇ
            else
            {
                Debug.DrawLine(player.Player_GamePiece.transform.position, new Vector3(dragOffset.x * direction.magnitude, 1, dragOffset.z * direction.magnitude), Color.black);
            }
        }
        // ��������
        else if (dragged.Value == true)
        {
            dragged.Value = false;

            // �ړ��ʂ�������
            rb.AddForce(-dragOffset * 27, ForceMode.Impulse);
        }
    }

    void IGameMode.FixUpdate()
    {
    }

    void IGameMode.Exit()
    {

    }
}