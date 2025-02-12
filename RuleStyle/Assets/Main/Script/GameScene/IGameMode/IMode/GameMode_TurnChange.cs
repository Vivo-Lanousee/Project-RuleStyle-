using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �^�[�����ύX���鎞�̏����B�N���A����̖��������B
/// </summary>
public class GameMode_TurnChange : IGameMode
{
        private GameSessionManager GameSceneManager;
        public GameMode_TurnChange(GameSessionManager gameSceneManager)
        {
            GameSceneManager = gameSceneManager;
        }
        
        /// <summary>
        /// �N���A���蓙���s���B
        /// </summary>
        void IGameMode.Init()
        {
            GameSceneManager= GameSessionManager.Instance();
    }

        void IGameMode.Update()
        {
        }


        void IGameMode.FixUpdate()
        {
        }

        void IGameMode.Exit()
        {

        }
        /// <summary>
        /// �N���A����
        /// </summary>
        private void Clear()
        {
        GameManager manager = GameManager.Instance();
            Debug.Log("�N���A����J�n");
            
            //�N���A�����v���C���[���ꎞ�I�ɕۑ�������̂Ƃ���B
            List<PlayerSessionData> clear_player= new List<PlayerSessionData>();
            List<PlayerSessionData> normal_player= new List<PlayerSessionData>();
            //�N���A����
            foreach (var i in GameSceneManager.Session_Data)
            {
                if (i.Value.PlayerPoint>= manager.ClearPoint)
                {
                       clear_player.Add(i.Value);
                }
                else if (i.Value.PlayerPoint < manager.ClearPoint)
                {
                       normal_player.Add(i.Value);
                }
            }
            //�N���A�v���C���[������ꍇ�A�W�v���s���A���U���g���[�h�ֈڍs����悤�ɂ���
            if(clear_player.Count>0)
            {
                //�_���̏��ԕt��
                clear_player = clear_player.OrderBy(player => player.PlayerPoint).ToList();
                normal_player = normal_player.OrderBy (player => player.PlayerPoint).ToList();
                
                //�����ŏ��ʕt�����ɍs���Ă��܂��B
                int num = 1;
                PlayerSessionData beforeplayer=null;
                int PaddingNum = 0;
                //PaddingPlayer�֕ϐ����i�[���邱�Ƃőؔ[����
                List<PlayerSessionData> PaddingPlayer=new List<PlayerSessionData>();

            //�N���A�҂��珇�ʕt�������Ă���
            foreach (var i in clear_player)
            {
                // �����_���̃v���C���[�����邩�ǂ������m�F
                if (beforeplayer != null && i.PlayerPoint == beforeplayer.PlayerPoint)
                {
                    PaddingPlayer.Add(i);  // �������ʂɒǉ�
                    PaddingNum++;//�������ʂł���΁B
                }
                else
                {
                    // �������ʂ̃v���C���[�������ꍇ�A���ʂ��X�V
                    if (PaddingPlayer.Count > 0)
                    {
                        foreach (var x in PaddingPlayer)
                        {
                            GameManager.Variable_Data[x.PlayerId].Ranking = num;
                        }
                        PaddingPlayer.Clear();
                        num += PaddingNum;
                        PaddingNum = 0;
                    }
                    //�������ʂ����炸�A�O��Ƃ͓_�����Ⴄ�ꍇ�i�ʏ�j
                    else
                    {
                        // ���݂̃v���C���[�����ʕt��
                        GameManager.Variable_Data[i.PlayerId].Ranking = num;
                        num++; // ���̏��ʂɐi��
                    }
                }
                beforeplayer = i;
            }

            // �Ō�̓������ʂ̏���
            if (PaddingPlayer.Count > 0)
            {
                foreach (var x in PaddingPlayer)
                {
                    GameManager.Variable_Data[x.PlayerId].Ranking = num - 1;
                }
            }


            //�N���A�ł��Ȃ������҂����ʕt��
            foreach (var i in normal_player)
            {
                // �������ʏ������s��
                if (i.PlayerPoint == beforeplayer.PlayerPoint)
                {
                    PaddingPlayer.Add(i);  // �������ʂɒǉ�
                    PaddingNum++;//�������ʂł���΁B
                }
                else
                {
                    // �������ʂ̃v���C���[�������ꍇ�A���ʂ��X�V
                    if (PaddingPlayer.Count > 0)
                    {
                        foreach (var x in PaddingPlayer)
                        {
                            GameManager.Variable_Data[x.PlayerId].Ranking = num;
                        }
                        PaddingPlayer.Clear();
                        num += PaddingNum;
                        PaddingNum = 0;
                    }
                    //�������ʂ����炸�A�O��Ƃ͓_�����Ⴄ�ꍇ�i�ʏ�j
                    else
                    {
                        // ���݂̃v���C���[�����ʕt��
                        GameManager.Variable_Data[i.PlayerId].Ranking = num;
                        num++; // ���̏��ʂɐi��
                    }
                }
                beforeplayer = i;
            }

            // �Ō�܂Ŏc�����������ʂ̏���
            if (PaddingPlayer.Count > 0)
            {
                foreach (var x in PaddingPlayer)
                {
                    GameManager.Variable_Data[x.PlayerId].Ranking = num - 1;
                }
            }

            //���U���g�ֈڍs����
            GameSceneManager.sceneContext.Mode_Change(new GameMode_Result());
        }
            else
            {
                //�Q�[�����[�h��ύX���A�J�[�h���σ��[�h�ֈڍs����B
                GameSceneManager.sceneContext.Mode_Change(new GameMode_ExchangeMode());
            }

        }
}
