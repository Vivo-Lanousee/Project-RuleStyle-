using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h�B�Ώێ҂̓J�[�h�v���C���[
/// </summary>
public class Card_Blue_MySelf : ICard,ICard_Blue
{
    public PlayerSessionData PlayerData { get; set; } = null;

    /// <summary>
    /// ��J�[�h�̉e���̈׃v���C���[�͂��̃J�[�h���������Ƃ͂Ȃ��i�̂�NULL�j
    /// </summary>
    public float? ProbabilityNum => null;
    Card_Pattern ICard.card_pattern => Card_Pattern.Blue;

    /// <summary>
    /// �J�[�h��
    /// </summary>
    string ICard.CardName => "�������g��";

    /// <summary>
    /// �J�[�hBlue�̎��݂̂̎����ƂȂ�B
    /// </summary>
    public List<int> EffectMember => new List<int> {};
    /// <summary>
    /// �͑S�ĕԂ�l�Ō��ʂ��s��
    /// </summary>
    void ICard.CardNum()
    {
        //�J�[�h�v���C���[���g�Ƀf�[�^���A��������i�v����
        //PlayerData.EffectPlayer_Id.Add(PlayerData.PlayerId);

        //�J�[�h�v���C���[��Ώۂɂ���B
        EffectMember.Add(PlayerData.PlayerId);
    }
}
