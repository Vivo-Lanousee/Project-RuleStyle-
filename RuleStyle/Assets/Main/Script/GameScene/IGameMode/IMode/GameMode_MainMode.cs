using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode_MainMode : IGameMode
{
    GameSessionManager GameSceneManager;

    PlayerSessionData player;

    //�J�����̈ʒu
    //public Transform Camera;
    /// <summary>
    /// ���S
    /// </summary>
    public Vector3 Cont = new Vector3(0, 0, 0);
    public int Dist = 10;
    public GameMode_MainMode(GameSessionManager gameSceneManager)
    {
        GameSceneManager = gameSceneManager;
    }

    /// <summary>
    /// �ύX���鐔�l
    /// </summary>
    public int ChangeMeter = 0;

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
    }

    /// <summary>
    /// �V���b�g�����쐬����B
    /// </summary>
    void IGameMode.Update()
    {
        CameraRole();
    }

    /// <summary>
    /// �J��������ƃJ�����̉�]
    /// </summary>
    void CameraRole()
    {
        //����l�ɍs���ΐ��l��߂�
        if (ChangeMeter >= 361)
        {
            ChangeMeter = 0;
        }
        if (ChangeMeter < 0)
        {
            ChangeMeter = 360;
        }
        //���l��ύX
        if (Input.GetMouseButton(1))
        {
            ChangeMeter++;
        }
        if (Input.GetMouseButton(2))
        {
            ChangeMeter--;
        }

        //���W�A���ϊ�
        float test = ChangeMeter * Mathf.Deg2Rad;
        //�ړ�����ׂ��n�_���Z�o
        float b = Mathf.Cos(test);
        float a = Mathf.Sin(test);
        //�n�_�쐬
        Vector3 t = new Vector3(b * Dist, 10, a * Dist);
        //�J�����̈ʒu��ύX
        GameSceneManager.CameraPosition.transform.position = t;

        Vector3 direction = Cont - t;
        
        // �J�����̉�]���v�Z
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // X ���̉�]�������I�ɐݒ�
        Vector3 eulerRotation = targetRotation.eulerAngles;
        eulerRotation.x = 30f; // �C�ӂ̊p�x�ɌŒ�A�Ⴆ��30�x
        targetRotation = Quaternion.Euler(eulerRotation);

        // ��]��K�p
        GameSceneManager.CameraPosition.rotation = targetRotation;

    }

    void IGameMode.FixUpdate()
    {
    }

    void IGameMode.Exit()
    {

    }
}