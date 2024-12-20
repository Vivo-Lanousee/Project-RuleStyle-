using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �f�[�^
/// </summary>
[Serializable]
public class PlayerData
{
    /// <summary>
    /// ���[���S��
    /// </summary>
    public string Rule { get; }

    /// <summary>
    /// �J�[�h�v�����
    /// </summary>
    public ICard card_One;
    /// <summary>
    /// �J�[�h�v�����
    /// </summary>
    public ICard card_Two;

    public ICard card_Three;

    public ICard card_Four;

    public ICard card_Five;
    /// <summary>
    /// �v���C���[�̋�
    /// </summary>
    public GameObject Player_GamePiece;
}