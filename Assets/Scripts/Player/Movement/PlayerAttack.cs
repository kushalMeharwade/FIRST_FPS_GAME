using Assets.Scripts.Player.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Player.Movement
{
    public  class PlayerAttack : MonoBehaviour
    {
        #region Varialbes

        float nextTimeToFire;
        public float fireReate = 15f;
        WeaponsManager Weapon_Manager;
        public bool is_Aiming = false;
        public Animator FPS_CAMERA_ANIMATOR;

        Camera Main_Cam;
        [SerializeField]
        public GameObject Cross_Hair_Image;

        [SerializeField]
        private GameObject Arrow_Prefab, Spear_Prefab;

        [SerializeField]
        private Transform ArrowStartPosition;

       

        #endregion
        private void Awake()
        {
            Weapon_Manager=GetComponent<WeaponsManager>();
            Main_Cam = Camera.main;
        }
        private void Start()
        {
            
        }

        public void BulletFired()
        {
            RaycastHit RayCastHitObject;
            if(Physics.Raycast(Main_Cam.transform.position,Main_Cam.transform.forward,out RayCastHitObject))
            {
               
                if (RayCastHitObject.transform.tag == TagManager.ENEMY_TAG)
                {
                    HealthScript health_object = RayCastHitObject.transform.GetComponent<HealthScript>();
                    health_object.ApplyDamage(Weapon_Manager.GetSelectedWeapon().damage);
                }
              

            }
        }

        void Shoot()
        {

            if (Weapon_Manager.GetSelectedWeapon().FireType == WeaponFireType.MULTIPLE)
            {
                if((Input.GetMouseButton(0) || Input.GetButtonDown("Shoot_Button")) && Time.time > nextTimeToFire){

                    nextTimeToFire = Time.time + 1f / fireReate;
                    Weapon_Manager.GetSelectedWeapon().Play_ShootAnimation();
                    Weapon_Manager.GetSelectedWeapon().Play_ShootSound();
                    BulletFired();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Shoot_Button"))
                {
                    if (Weapon_Manager.GetSelectedWeapon().gameObject.tag == TagManager.AXE_TAG)
                    {
                        Weapon_Manager.GetSelectedWeapon().Play_ShootAnimation();
                       
                       
                    }


                    if (Weapon_Manager.GetSelectedWeapon().BulletType == WeaponBulletType.BULLET)
                    {
                        Weapon_Manager.GetSelectedWeapon().Play_ShootAnimation();
                      
                        BulletFired();
                    }
                    else
                    {
                        if (is_Aiming)
                        {
                            if (Weapon_Manager.GetSelectedWeapon().gameObject.tag == TagManager.SPEAR_TAG)
                            {
                                Weapon_Manager.GetSelectedWeapon().Play_ShootAnimation();
                               
                                

                                GameObject SPEAR_GAMEOBJECT=Instantiate(Spear_Prefab);
                                SPEAR_GAMEOBJECT.transform.position = ArrowStartPosition.transform.position;
                                SPEAR_GAMEOBJECT.GetComponent<BowAndSpearScript>().Launch(Main_Cam);

                            }
                            else if (Weapon_Manager.GetSelectedWeapon().gameObject.tag == TagManager.BOW_TAG)
                            {
                                Weapon_Manager.GetSelectedWeapon().Play_ShootAnimation();
                             


                                GameObject SPEAR_GAMEOBJECT = Instantiate(Arrow_Prefab);
                                SPEAR_GAMEOBJECT.transform.position = ArrowStartPosition.transform.position;
                                SPEAR_GAMEOBJECT.GetComponent<BowAndSpearScript>().Launch(Main_Cam);


                            }
                        }
                    }
                }

              



            }

        }


        void Aim()
        {
            if (Input.GetMouseButtonDown(1) && Weapon_Manager.GetSelectedWeapon().AimType==WeaponAimType.AIM)
            {
                if (!is_Aiming)
                {
                    FPS_CAMERA_ANIMATOR.SetTrigger(TagManager.FPS_CAMERA_AIM_IN);
                    is_Aiming = true;
                    Cross_Hair_Image.SetActive(false);
                }
                else
                {
                    FPS_CAMERA_ANIMATOR.SetTrigger(TagManager.FPS_CAMERA_AIM_OUT);
                    is_Aiming = false;
                    Cross_Hair_Image.SetActive(true);
                }
            }else if(Input.GetMouseButtonDown(1) && Weapon_Manager.GetSelectedWeapon().AimType == WeaponAimType.SELF_AIM)
            {
                if (!is_Aiming)
                {
                    Weapon_Manager.GetSelectedWeapon().Play_AimAnimation(true);
                    is_Aiming = true;

                }
                else
                {
                    Weapon_Manager.GetSelectedWeapon().Play_AimAnimation(false);
                    is_Aiming = false;
                }
            }
          
        }

    


        private void Update()
        {
            if (Input.GetButtonDown("Shoot_Button"))
            {
                Debug.Log("Button Clicked");
            }

            Aim();
            Shoot();
        }
    }
}
