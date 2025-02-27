using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���_���擾����B
/// </summary>
public class Card_Yellow_Point : ICard
{
    public PlayerSessionData PlayerData { get; set; } = null;

    public float? ProbabilityNum => null;
    Card_Pattern ICard.card_pattern => Card_Pattern.Yellow;

    string ICard.CardName => "���_";
    Sprite ICard.cardUI { get; set; }
    /// <summary>
    /// �l���[�����������s�B���_��ύX����
    /// </summary>
    void ICard.CardNum()
    {
        if (PlayerData!=null)
        {
            GameSessionManager manage=GameSessionManager.Instance();
            if (PlayerData.RuleSuccessCalculation=="+")
            {
                //EffectAward��V�ΏۑS�ĂɃv���C���[�̓_����ύX����
                foreach (var a in PlayerData.EffectAwardPlayer_Id)
                {
                    manage.Session_Data[a].PlayerPoint += PlayerData.RuleSuccessNum;
                }
            }
            else if(PlayerData.RuleSuccessCalculation=="-") 
            {
                foreach (var a in PlayerData.EffectAwardPlayer_Id)
                {
                    manage.Session_Data[a].PlayerPoint -= PlayerData.RuleSuccessNum;
                    //0����������ꍇ������0�ɕύX����B
                    if (manage.Session_Data[a].PlayerPoint < 0) { manage.Session_Data[a].PlayerPoint = 0; }
                }

                
            }
        }
    }
}
