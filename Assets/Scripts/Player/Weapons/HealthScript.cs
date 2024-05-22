using Assets.Scripts.Enemy;
using Assets.Scripts.Player.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Player.Weapons
{
    public  class HealthScript:MonoBehaviour
    {
        #region Variable Declarations

        public NavMeshAgent _NavMeshAgent;
        public Enemy_Animator _Animator;
        public Enemy_Controller _EnemyController;
        public float Health = 100f;
        public bool isPlayer, isCanibal, isBoar;
        public  bool isDead = false;
        private Enemy_Audio _enemyAudio;
        private PlayerStats PlayerStats;
        private void Awake()
        {
            if(isBoar || isCanibal)
            {
                _Animator = GetComponent<Enemy_Animator>();
                _NavMeshAgent = GetComponent<NavMeshAgent>();
                _EnemyController=GetComponent<Enemy_Controller>();
                _enemyAudio=GetComponentInChildren<Enemy_Audio>();

            }

            if (isPlayer)
            {
                PlayerStats = GetComponent<PlayerStats>();
            }
           
        }

        public void ApplyDamage(float damage)
        {
            if (isDead == true)
                return;
            if(Health <= 0f)
            {
                isDead = true;
                if (isBoar || isCanibal)
                {
                    _Animator.EnemyDied(true);
                }
                else
                {
                    PlayerDied();
                }
               
                
            }
            else
            {
                Health -= damage;
                if (isBoar || isCanibal)
                {
                    _Animator.hasBeenHit();
                    if (_EnemyController.CurrentEnemeyState == ENEMY_STATE.PATROL)
                    {
                        _EnemyController.chase_distance = 50f;
                    }
                }
                if (isPlayer)
                {
                    PlayerStats.UpdateHealth(Health);
                }
            }
        }


        private void PlayerDied()
        {
         

            if (isPlayer)
            {
                Debug.Log("Player Died");

                GameObject[] enemies = GameObject.FindGameObjectsWithTag(TagManager.ENEMY_TAG);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<Enemy_Controller>().enabled = false;
                }

                // call enemy manager to stop spawning enemis
                GetComponent<PlayerStats>().DisableWeapons();
               



              
                GameObject Enemy_ManagerObject = GameObject.Find("Enemy_Manager");
                if(Enemy_ManagerObject != null)
                {
                    Enemy_ManagerObject.SetActive(false);
                }




            }
           
        }


        IEnumerator DeadSound()
        {
            yield return new WaitForSeconds(3);
            _enemyAudio.PlayDeathSound();

        }


        #endregion

    }
}
