using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public  class PlayWooshSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private AudioClip[] wooshSounds;


        public void PlayWooshSound_Track()
        {
            int random_index_wooshSound=UnityEngine.Random.Range(0,wooshSounds.Length);
            _audioSource.clip = wooshSounds[random_index_wooshSound];
            _audioSource.Play();
        }
        
    }
}
