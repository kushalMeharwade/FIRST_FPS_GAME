using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;



namespace Assets.Scripts.Player.Movement
{
    public  class FootStepSoundManger : MonoBehaviour
    {

        public AudioSource FootStepAudioPlayer;
        [SerializeField]
        private AudioClip[] FootStepSoundClips;
        [SerializeField]
        public  float minVolumne, maxVolume;



        private float AccumulatedDistance;
        public  float StepDistance;
        private CharacterController Player_CharacterController;
        private void Start()
        {
            FootStepAudioPlayer = GetComponent<AudioSource>();
            Player_CharacterController = GetComponentInParent<CharacterController>();
        }

        private void CheckAndPlaySoundEffect()
        {
            if (!Player_CharacterController.isGrounded) return;

            if(Player_CharacterController.velocity.sqrMagnitude > 0)
            {
                AccumulatedDistance += Time.deltaTime;
                if(AccumulatedDistance > StepDistance)
                {
                    PlaySoundEffect();
                    AccumulatedDistance = 0;
                }
            }
            else
            {
                AccumulatedDistance = 0f;
            }

        }

        private void PlaySoundEffect()
        {
            FootStepAudioPlayer.volume = Random.Range(minVolumne, maxVolume);
            FootStepAudioPlayer.clip = FootStepSoundClips[Random.Range(0, FootStepSoundClips.Length)];
            FootStepAudioPlayer.Play();

        }

        private void Update()
        {
            CheckAndPlaySoundEffect();
        }
    }
}
