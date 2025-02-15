using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;




/// <summary>
/// �J�[�h�ύX�V�[���̐ݒ�B
/// </summary>
public class GameMode_ExchangeMode : IGameMode
{
    private GameSessionManager sessionManager;

    ExChangeComponent ExChange;

    private ReactiveProperty<int> Cost=new ReactiveProperty<int>(3);

    /// <summary>
    /// ���݂̕ύX�v���C��-���i�[����B
    /// </summary>
    private PlayerSessionData Change_CurrentPlayer;

    private ChangeData Changes=null;

    private ICard HandCards=null;

    private IDisposable CostEvent;

    /// <summary>
    /// �J�[�h�̏��ƃC�x���g�t����S�čs���B
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    void IGameMode.Init()
    {
        sessionManager = GameSessionManager.Instance();
        if (sessionManager.ExchangeMember.Count ==0)
        {
            sessionManager.sceneContext.Mode_Change(new GameMode_TurnChange(sessionManager));
            Debug.Log("���C�����[�h�ɖ߂�");
            Debug.Log("�f�o�b�O�p�F�c��" + sessionManager.ExchangeMember.Count + "�l");
        }
        else if(sessionManager.ExchangeMember.Count > 0)
        {
            ExChange=sessionManager.ChangeScene.GetComponent<ExChangeComponent>();

            int current = sessionManager.ExchangeMember.First.Value;
            sessionManager.ExchangeMember.RemoveFirst();
            Change_CurrentPlayer = sessionManager.Session_Data[current];

            sessionManager.ChangeScene.SetActive(true);

            AddOnClick(Change_CurrentPlayer);

            AllLoadUI();

            //���݃v���C���[�̕\������
            sessionManager.ChangeScene.GetComponent<ExChangeComponent>().CurrentPlayerImage.sprite = sessionManager.card_Access["P" + Change_CurrentPlayer.PlayerId.ToString() + "��"].cardUI;
            HandLoad();


            //�J�[�h�ɑ΂���C�x���g�t���J�n�B
            HandCardDataOnClick();
            AllLoadCardDataOnClick();

            CostEvent=Cost.Subscribe(_ =>
            {
            ExChangeComponent ex = sessionManager.ChangeScene.GetComponent<ExChangeComponent>();
            switch (_)
                {
                    case 0:
                        ex.Cost_One.gameObject.SetActive(false);
                        ex.Cost_Two.gameObject.SetActive(false);
                        ex.Cost_Three.gameObject.SetActive(false);
                        break;
                    case 1:
                        ex.Cost_One.gameObject.SetActive(true);
                        ex.Cost_Two.gameObject.SetActive(false);
                        ex.Cost_Three.gameObject.SetActive(false);
                        break; 
                    case 2:
                        ex.Cost_One.gameObject.SetActive(true);
                        ex.Cost_Two.gameObject.SetActive(true);
                        ex.Cost_Three.gameObject.SetActive(false);
                        break; 
                    case 3:
                        ex.Cost_One.gameObject.SetActive(true);
                        ex.Cost_Two.gameObject.SetActive(true);
                        ex.Cost_Three.gameObject.SetActive(true);
                        break;
                }
        });
        }

        
    }
    void IGameMode.Exit()
    {
        CostEvent?.Dispose();
    }
    void IGameMode.FixUpdate()
    {
    }
    void IGameMode.Update()
    {
    }

    void AllLoadUI()
    {
        foreach (var x in sessionManager.Session_Data)
        {
            switch (x.Value.PlayerId)
            {
                case 1:
                    LoadUI(ExChange.Player_One, x.Value);
                    break;
                case 2:
                    LoadUI(ExChange.Player_Two, x.Value);
                    break;
                case 3:
                    LoadUI(ExChange.Player_Three, x.Value);
                    break;
                case 4:
                    LoadUI(ExChange.Player_Four, x.Value);
                    break;
            }
        }
    }
    /// <summary>
    /// �v���C���[�P�ʂ�UI�ύX
    /// </summary>
    /// <param name="UICompo"></param>
    void LoadUI(Rule_UI_RuleComponent UIComponent, PlayerSessionData playerdata)
    {
        GameSessionManager manager = GameSessionManager.Instance();
        //�v���C���[UI�ύX�i�������łɕύX�����̗p�ӂ�������������������Ȃ�
        UIComponent.PlayerImage.sprite = manager.card_Access["P" + playerdata.PlayerId.ToString() + "��"].cardUI;

        //�J�[�h��UI�ύX(�Z�b�V�����f�[�^�Ŋ��Ƀ��[�h����Ă���f�[�^�ׁ̈A��{�I�ɃJ�[�h�̓A�^�b�`���邾���Ŗ��Ȃ�
        UIComponent.Red_Card_EffectPiece.image.sprite = playerdata.Card_Red_EffectPiece.Value.cardUI;
        UIComponent.Blue_Card.image.sprite = playerdata.Card_Blue.Value.cardUI;
        UIComponent.Red_Card_EffectAward.image.sprite = playerdata.Card_Red_EffectAward.Value.cardUI;
        UIComponent.Yellow_Card.image.sprite = playerdata.Card_Yellow.Value.cardUI;
        UIComponent.Green_Card.image.sprite = playerdata.Card_Green.Value.cardUI;
        UIComponent.Purple_Card.image.sprite = playerdata.Card_Purple.Value.cardUI;

        //���[�����ύX
        UIComponent.RuleText.text = playerdata.RuleText_Exchange();
    }

    /// <summary>
    /// ��D�ȊO�̃v���C���[�̃C�x���g�t��(��ʂ�̂݁j
    /// </summary>
    void LoadCardDataOnClick(Rule_UI_RuleComponent UIComponent,int num)
    {
        UIComponent.Red_Card_EffectPiece.onClick.RemoveAllListeners();
        UIComponent.Blue_Card.onClick.RemoveAllListeners();
        UIComponent.Red_Card_EffectAward.onClick.RemoveAllListeners();
        UIComponent.Yellow_Card.onClick.RemoveAllListeners();
        UIComponent.Green_Card.onClick.RemoveAllListeners();
        UIComponent.Purple_Card.onClick.RemoveAllListeners();

        UIComponent.Red_Card_EffectPiece.onClick.AddListener(() =>
        {
            if (Changes == null)
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 1
                };
            }
            else if (Changes.data == sessionManager.Session_Data[num] && Changes.card_num == 1)
            {
                Changes = null;
            }
            else
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 1
                };
            }
        });
        UIComponent.Blue_Card.onClick.AddListener(() =>
        {
            if (Changes == null)
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 2
                };
            }
            else if (Changes.data == sessionManager.Session_Data[num] && Changes.card_num == 2)
            {
                Changes = null;
            }
            else
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 2
                };
            }
        });
        UIComponent.Red_Card_EffectAward.onClick.AddListener(() =>
        {
            if (Changes == null)
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 3
                };
            }
            else if (Changes.data == sessionManager.Session_Data[num] && Changes.card_num == 3)
            {
                Changes = null;
            }
            else
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 3
                };
            }
        });
        UIComponent.Yellow_Card.onClick.AddListener(() =>
        {
            if (Changes == null)
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 4
                };
            }
            else if (Changes.data == sessionManager.Session_Data[num] && Changes.card_num == 4)
            {
                Changes = null;
            }
            else
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 4
                };
            }
        });
        UIComponent.Green_Card.onClick.AddListener(() =>
        {
            if (Changes == null)
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 5
                };
            }
            else if (Changes.data == sessionManager.Session_Data[num] && Changes.card_num == 5)
            {
                Changes = null;
            }
            else
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 5
                };
            }
        });
        UIComponent.Purple_Card.onClick.AddListener(() =>
        {
            if (Changes == null)
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 6
                };
            }
            else if(Changes.data == sessionManager.Session_Data[num] && Changes.card_num == 6)
            {
                Changes = null;  
            }
            else
            {
                Changes = new ChangeData
                {
                    data = sessionManager.Session_Data[num],
                    card_num = 6
                };
            }
            
        });
    }

    /// <summary>
    /// ��D�̃f�[�^�i�C�x���g�j��S�ēo�^����
    /// </summary>
    void HandCardDataOnClick()
    {
        int num = 0;
        foreach(var x in Change_CurrentPlayer.HandCards)
        {
            switch (num)
            {
                case 0:
                    sessionManager._HandCard_Component.card_one.onClick.RemoveAllListeners();
                    sessionManager._HandCard_Component.card_one.onClick.AddListener(() => 
                    {
                        HandCards = x;
                    });
                    break; 
                case 1:
                    sessionManager._HandCard_Component.card_two.onClick.RemoveAllListeners();
                    sessionManager._HandCard_Component.card_two.onClick.AddListener(() =>
                    {
                        HandCards = x;
                    });
                    break;
                case 2:
                    sessionManager._HandCard_Component.card_three.onClick.RemoveAllListeners();
                    sessionManager._HandCard_Component.card_three.onClick.AddListener(() =>
                    {
                        HandCards = x;
                    });
                    break; 
                case 3:
                    sessionManager._HandCard_Component.card_four.onClick.RemoveAllListeners();
                    sessionManager._HandCard_Component.card_four.onClick.AddListener(() =>
                    {
                        HandCards = x;
                    });
                    break;
                case 4:
                    sessionManager._HandCard_Component.card_five.onClick.RemoveAllListeners();
                    sessionManager._HandCard_Component.card_five.onClick.AddListener(() =>
                    {
                        HandCards = x;
                    });
                    break;
            }
            num++;
        }
        
    }
    /// <summary>
    /// �ێ��J�[�h�̃C�x���g��S�ēo�^�����B
    /// </summary>
    void AllLoadCardDataOnClick()
    {
         List<int> list = new List<int>() { 1,2,3,4};

        foreach (int i in list)
        {
            switch (i)
            {
                case 1:
                    LoadCardDataOnClick(ExChange.Player_One, i);
                    break; 
                case 2:
                    LoadCardDataOnClick(ExChange.Player_Two, i);
                    break; 
                case 3:
                    LoadCardDataOnClick(ExChange.Player_Three, i);
                    break;
                case 4:
                    LoadCardDataOnClick(ExChange.Player_Four, i);
                    break;
            }
        }
    }


    void HandLoad()
    {
        int num = 1;
        foreach (var a in Change_CurrentPlayer.HandCards)
        {
            switch (num)
            {
                case 1:
                    sessionManager._HandCard_Component.card_one.image.sprite = a.cardUI;
                    break;
                case 2:
                    sessionManager._HandCard_Component.card_two.image.sprite = a.cardUI;
                    break;
                case 3:
                    sessionManager._HandCard_Component.card_three.image.sprite = a.cardUI;
                    break;
                case 4:
                    sessionManager._HandCard_Component.card_four.image.sprite = a.cardUI;
                    break;
                case 5:
                    sessionManager._HandCard_Component.card_five.image.sprite = a.cardUI;
                    break;
            }

            num++;
        }
        
    }

    /// <summary>
    /// �O���ꍇ�B
    /// </summary>
    void CardRemove()
    {
        //�R�X�g����
        if (Changes.data==Change_CurrentPlayer)
        {
            if (Cost.Value-1 >=0)
            {
                Cost.Value -= 1;
            }
            else
            {
                return;
            }
            
        }
        else
        {
            if (Cost.Value  - 2 >= 0)
            {
                Cost.Value -= 2;
            }
            else
            {
                return;
            }
        }

        //���s����
        switch (Changes.card_num)
        {
            case 1:
                Changes.data.Remove_Red_EffectPiece();
                break;
            case 2:
                Changes.data.Remove_Blue();
                break;
            case 3:
                Changes.data.Remove_Red_EffectAward();
                break;
            case 4:
                Changes.data.Remove_Yellow();
                break;
            case 5:
                Changes.data.Remove_Green();
                break;
            case 6:
                Changes.data.Remove_Purple();
                break;
        }

        Changes = null;

        AllLoadUI();
    }
    /// <summary>
    /// �J�[�h���ύX����鎞�B
    /// </summary>
    void CardChange()
    {
        switch (Changes.card_num)
        {
            case 1:
                if (HandCards.card_pattern == Card_Pattern.Red)
                {
                    Changes.data.Card_Red_EffectPiece.Value = HandCards;

                    HandCards = null;
                }
                break;
            case 2:
                if (HandCards.card_pattern == Card_Pattern.Blue)
                {
                    Changes.data.Card_Blue.Value = HandCards;

                    HandCards = null;
                }
                break;
            case 3:
                if (HandCards.card_pattern == Card_Pattern.Red)
                {

                    Changes.data.Card_Red_EffectAward.Value = HandCards;

                    HandCards = null;
                }
                break;
            case 4:
                if (HandCards.card_pattern == Card_Pattern.Yellow)
                {

                    Changes.data.Card_Yellow.Value = HandCards;

                    HandCards = null;
                }
                break;
            case 5:
                if (HandCards.card_pattern == Card_Pattern.Green)
                {
                    Changes.data.Card_Green.Value = HandCards;

                    HandCards = null;
                }
                
                break;
            case 6:
                if (HandCards.card_pattern == Card_Pattern.Purple)
                {
                    Changes.data.Card_Purple.Value = HandCards;

                    HandCards = null;
                }
                
                break;
        }
    }
    /// <summary>
    /// �v���C���[�f�[�^���Q�Ƃ�UI�ɃC�x���g��t���čs�����
    /// </summary>
    public void AddOnClick(PlayerSessionData playerdata)
    {
        ExChange.ExChange_or_Remove_StartButton.onClick.RemoveAllListeners();
        ExChange.ExChange_or_Remove_StartButton.onClick.AddListener(() => 
        { 
            if(HandCards != null && Changes!=null)
            {
                CardChange();
                Debug.Log("�f�[�^�ύX����");
            }
            else if(HandCards == null && Changes!=null) 
            {
                CardRemove();
                Debug.Log("�f�[�^�폜����");
            }
            else
            {
                Debug.Log("�����I������Ă��Ȃ�");
            }
        });

        ExChange.ExChangeEndButton.onClick.RemoveAllListeners();
        ExChange.ExChangeEndButton.onClick.AddListener(() => 
        {
            sessionManager.ChangeScene.SetActive(false);
            //�ēx�ύX���[�h��ǂݍ��ނ��ƂőS�ď�������B
            sessionManager.sceneContext.Mode_Change(new GameMode_ExchangeMode());
        });

        //.Player_One.
    }
}

