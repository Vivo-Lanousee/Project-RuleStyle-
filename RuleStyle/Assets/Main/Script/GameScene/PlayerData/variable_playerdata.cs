using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ω\�ȃv���C���[�f�[�^(���O���j
/// �S�V�[���ł̋��ʃf�[�^��ۑ�����ׂ̃N���X�Ƃ��Ďg�p����iGameManager�ŕۊǂ��Ă����j
/// </summary>
public class variable_playerdata
{
    /// <summary>
    /// ���������l
    /// </summary>
    /// <param name="NewName"></param>
    public variable_playerdata(int ID,string NewName)
    {
        Id = ID;
        PlayerName = NewName;
    }
    public int Id;
    public string PlayerName;

    
}