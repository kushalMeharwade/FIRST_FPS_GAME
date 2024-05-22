using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Enemy_Audio :MonoBehaviour
    {

        #region variable Declarations

        private AudioSource _audioSource;
        [SerializeField]
        private AudioClip ScreamSound, DeathSound;

        [SerializeField]
        private AudioClip[] AttackSounds;

        #endregion

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public  void PlayDeathSound()
        {
            _audioSource.clip = DeathSound;
            _audioSource.Play();
        }

        public void PlayScreamSound()
        {
            _audioSource.clip = ScreamSound;
            _audioSource.Play();
        }

        public void PlayAttackSound()
        {
            int random_index_sound_clip=UnityEngine.Random.Range(0,AttackSounds.Length);
            _audioSource.clip = AttackSounds[random_index_sound_clip];
            _audioSource.Play();
        }

    }
}
