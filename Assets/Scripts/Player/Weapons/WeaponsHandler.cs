using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{

    public enum WeaponAimType
    {
        AIM,
        SELF_AIM,
        NONE
    };

    public enum WeaponFireType
    {
        SINGLE,
        MULTIPLE
    };

    public enum WeaponBulletType
    {
        BULLET,
        
        ARROW,
        SPEAR,
        NONE
    };
    public class WeaponsHandler : MonoBehaviour
    {

        #region Varialbe Declarations
        public WeaponAimType AimType;
        public WeaponBulletType BulletType;
        public WeaponFireType FireType;

        public GameObject Muzzle_Flash;
        public GameObject AttackPoint;
        public AudioSource ShootSound, ReloadSound;
        public Animator Weapon_Animator;
        public float damage;

        public bool isReadyToAttack = true;
        #endregion

        public void Play_ShootSound()
        {
            if (ShootSound == null) return;
            ShootSound.Play();
        }

        public void Play_ReloadSound()
        {
            if(ReloadSound== null) return;  
            ReloadSound.Play();
        }
        public void Play_ShootAnimation()
        {
            if (isReadyToAttack)
            {
                Weapon_Animator.SetTrigger(TagManager.ATTACK_TRIGGER);

            }
        }

        public void Play_AimAnimation(bool canAim)
        {
            Weapon_Animator.SetBool(TagManager.AIM_TRIGGER,canAim);
        }

        public void TurnOnMuzzleFlash()
        {
            Muzzle_Flash.SetActive(true);
        }

        public void TurnOfMuzzleFlash()
        {
            if (Muzzle_Flash.activeInHierarchy)
            {
                Muzzle_Flash.SetActive(false);
            }
        }

        public void TurnOnAttackPoint()
        {
            AttackPoint.SetActive(true);
        }

        public void SetReadyToAttackTrue()
        {
            this.isReadyToAttack = true;
        } 

        public void SetReadyToAttackFalse()
        {
            this.isReadyToAttack = false;
        }

        public void TurnOffAttackPoint()
        {
            if (AttackPoint.activeInHierarchy)
            {
                AttackPoint.SetActive(false);
            }
        }
    }
}
