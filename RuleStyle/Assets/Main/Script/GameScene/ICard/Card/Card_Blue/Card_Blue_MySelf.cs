using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h�B�Ώێ҂̓J�[�h�v���C���[
/// </summary>
public class Card_Blue_MySelf : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    /// <summary>
    /// ��J�[�h�̉e���̈׃v���C���[�͂��̃J�[�h���������Ƃ͂Ȃ��i�̂�NULL�j
    /// </summary>
    public int? ProbabilityNum => null;
    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�������g��";

    
    /// <summary>
    /// �͑S�ĕԂ�l�Ō��ʂ��s��
    /// </summary>
    void ICard.CardNum()
    {
        //�J�[�h�v���C���[���g�Ƀf�[�^���A��������i�v����
        PlayerData.EffectPlayer_Id.Add(PlayerData.PlayerId);
    }
}
