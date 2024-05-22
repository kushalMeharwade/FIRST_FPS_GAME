using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public  class WeaponsManager : MonoBehaviour 
    {
        public WeaponsHandler[] Weapons_Stack;
        public int ActiveWeaponIndex = 0;

        private void Start()
        {
            DisableAllWeapons();
            ActiveWeaponIndex = 0;
            Weapons_Stack[ActiveWeaponIndex].gameObject.SetActive(true);
        }

        void DisableAllWeapons()
        {
            for(int i=0;i<Weapons_Stack.Length;i++)
            {
                Weapons_Stack[i].gameObject.SetActive(false);
            }
        }

        private void ChangeActiveWeapon(int WeaponIndex)
        {
            if (WeaponIndex > Weapons_Stack.Length) return;
            if (WeaponIndex == ActiveWeaponIndex) return;

            Weapons_Stack[ActiveWeaponIndex].gameObject.SetActive(false);
            Weapons_Stack[WeaponIndex].gameObject.SetActive(true);
            ActiveWeaponIndex = WeaponIndex;
        }

        public WeaponsHandler GetSelectedWeapon()
        {
            return Weapons_Stack[ActiveWeaponIndex];
        }

        private void WeaponsChageEventHandler()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeActiveWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeActiveWeapon(1);

            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeActiveWeapon(2);

            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeActiveWeapon(3);

            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ChangeActiveWeapon(4);

            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                ChangeActiveWeapon(5);

            }
        }


        private void Update()
        {
            WeaponsChageEventHandler();
        }
    }
}
