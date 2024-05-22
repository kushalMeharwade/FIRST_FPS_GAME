using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Movement
{
    public class PlayerCroutch : MonoBehaviour
    {

        public float Standing_Height = 1.6f;
        public float Croutch_Height = 1f;
     
        public  bool isCrouching=false;

        public float Sprint_Speed = 10f;
        public float Crouch_Speed = 2f;
        public float Default_Speed = 5f;
        private PlayerMovement PlayerMovement;
        public Transform LookRoot;


        #region Movement Speed and Volume Settings
         private FootStepSoundManger PlayerFootStepSoundManger;

        private float Sprint_Volume = 1f;
        private float Crouch_Volume = 0.1f;
        private float Walk_Volume_min = 0.1f, Walk_Volume_Max = 0.6f;
        private float Walk_Step_Distance = 0.4f;
        private float Sprint_Step_Distance = 0.25f;
        private float Crouch_Step_Distance = 0.5f;


        public float sprint_value = 100f;
        public float sprint_threshold = 35f;
        PlayerStats PlayerHealthManager;
        #endregion


        private void Awake()
        {
            PlayerMovement = GetComponent<PlayerMovement>();
            PlayerFootStepSoundManger = GetComponentInChildren<FootStepSoundManger>();
            PlayerHealthManager = GetComponent<PlayerStats>();
        }
        private void Start()
        {
            PlayerFootStepSoundManger.minVolumne = Walk_Volume_min;
            PlayerFootStepSoundManger.maxVolume = Walk_Volume_Max;
            PlayerFootStepSoundManger.StepDistance= Walk_Step_Distance;


        }

      

        public void UpdateRun()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
               
                Debug.Log("SPRINT VALUE : " + sprint_value);
                PlayerMovement.Speed = Sprint_Speed;
                PlayerFootStepSoundManger.minVolumne = Sprint_Volume;
                PlayerFootStepSoundManger.maxVolume = Sprint_Volume;
                PlayerFootStepSoundManger.StepDistance = Sprint_Step_Distance;

                if (sprint_value <= 0f)
                {

                    sprint_value = 0f;
                    PlayerMovement.Speed = Default_Speed;
                    PlayerFootStepSoundManger.minVolumne = Walk_Volume_min;
                    PlayerFootStepSoundManger.maxVolume = Walk_Volume_Max;
                    PlayerFootStepSoundManger.StepDistance = Walk_Step_Distance;
                    Debug.Log("value reset");

                }

            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
            {
                PlayerMovement.Speed = Default_Speed;
                PlayerFootStepSoundManger.minVolumne = Walk_Volume_min;
                PlayerFootStepSoundManger.maxVolume = Walk_Volume_Max;
                PlayerFootStepSoundManger.StepDistance = Walk_Step_Distance;



            }

            if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
            {
                sprint_value -= sprint_threshold * Time.deltaTime;
                Debug.Log("SPRINT VALUE : " + sprint_value);
                if (sprint_value <= 0f)
                {

                    sprint_value = 0f;
                    PlayerMovement.Speed = Default_Speed;
                    PlayerFootStepSoundManger.minVolumne = Walk_Volume_min;
                    PlayerFootStepSoundManger.maxVolume = Walk_Volume_Max;
                    PlayerFootStepSoundManger.StepDistance = Walk_Step_Distance;
                    Debug.Log("value reset");

                }
            }
            else
            {
                if(sprint_value != 100f)
                {
                    sprint_value += (sprint_threshold / 2f) * Time.deltaTime;
                    PlayerHealthManager.UpdateStamina(sprint_value);
                    if (sprint_value > 100f)
                    {
                        sprint_value = 100f;
                    }
                    PlayerHealthManager.UpdateStamina(sprint_value);
                }
              
            }
            PlayerHealthManager.UpdateStamina(sprint_value);
        }

        public void UpdateCrouch()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                
                    if (isCrouching)
                    {
                        LookRoot.localPosition = new Vector3(0f, Standing_Height, 0f);
                        isCrouching = false;
                        PlayerMovement.Speed = Default_Speed;
                        PlayerFootStepSoundManger.minVolumne = Walk_Volume_min;
                        PlayerFootStepSoundManger.maxVolume = Walk_Volume_Max;
                        PlayerFootStepSoundManger.StepDistance = Walk_Step_Distance;
                     }
                    else
                    {
                        LookRoot.localPosition = new Vector3(0f, Croutch_Height, 0f);
                        isCrouching = true;
                        PlayerMovement.Speed = Crouch_Speed;
                        PlayerFootStepSoundManger.minVolumne = Crouch_Volume;
                        PlayerFootStepSoundManger.maxVolume = Crouch_Volume;
                        PlayerFootStepSoundManger.StepDistance = Crouch_Step_Distance;
                }
                }
            
           
          
        }
       
        private void Update()
        {
            UpdateCrouch();
            UpdateRun();
        }
    }
}
