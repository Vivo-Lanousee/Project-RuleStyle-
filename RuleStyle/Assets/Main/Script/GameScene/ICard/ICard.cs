using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h���
/// </summary>
public interface ICard
{
    public Card_Pattern card_pattern { get; }

    /// <summary>
    /// �J�[�h�̕���
    /// </summary>
    string CardName { get;}

    /// <summary>
    /// �J�[�h����
    /// </summary>
    void CardNum();
}

/// <summary>
/// �p�^�[��
/// </summary>
public enum Card_Pattern
{
    Blue,
    Orange,
    Green,
    Red
}