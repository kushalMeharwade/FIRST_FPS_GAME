using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Properties;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Enemy_Manager : MonoBehaviour
    {
        public static Enemy_Manager instance;
        public GameObject boarPrefab, Canibal_Prefab;
        public Transform[] Enemy_SpawnLocations;

        public int enemy_boar_count=0, enemy_canibal_count = 0;
        public int initail_boar_count, initial_canibal_count;

        public float Wait_Before_Next_Spawn = 10f;

        private void Awake()
        {
            MakeInstance();
        }

        void Start()
        {
            initail_boar_count = enemy_boar_count;
            initial_canibal_count = enemy_canibal_count;
            SpawnEnemies();

            StartCoroutine("CheckToSpawnEnemies");
        }

        private void SpawnEnemies()
        {
            SpawnBoar();
            SpawnCanibal();
        }

        private void SpawnBoar()
        {
            for(int i = 0; i < enemy_boar_count; i++)
            {
                int random_index_value = UnityEngine.Random.Range(0, Enemy_SpawnLocations.Length);
                Instantiate(boarPrefab, Enemy_SpawnLocations[random_index_value].position, Quaternion.identity);
            }

            enemy_boar_count = 0;
        }

        public void EnemyDied(bool cannibal)
        {

            if (cannibal)
            {

                enemy_canibal_count++;

                if (enemy_canibal_count > initial_canibal_count)
                {
                    enemy_canibal_count = initial_canibal_count;
                }

            }
            else
            {

                enemy_boar_count++;

                if (enemy_boar_count > initail_boar_count)
                {
                    enemy_boar_count = initail_boar_count;
                }

            }

        }
        private void SpawnCanibal()
        {
            for (int i = 0; i < enemy_canibal_count; i++)
            {
                int random_index_value = UnityEngine.Random.Range(0, Enemy_SpawnLocations.Length);
                Instantiate(Canibal_Prefab, Enemy_SpawnLocations[random_index_value].position, Quaternion.identity);
            }

            enemy_canibal_count = 0;
        }

        private void MakeInstance()
        {
            if (instance == null)
            {
                instance=new Enemy_Manager();
            }
        }


        IEnumerator CheckToSpawnEnemies()
        {
            yield return new WaitForSeconds(Wait_Before_Next_Spawn);
            SpawnBoar();
            SpawnCanibal();
            StartCoroutine("CheckToSpawnEnemies");
        }

        public void StopSpawning()
        {
            StopCoroutine("CheckToSpawnEnemies");
        }
    }
}
