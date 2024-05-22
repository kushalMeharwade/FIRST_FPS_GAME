using Assets.Scripts.Player.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update


    public Image Health_bar, Stamina_Bar;
    public GameObject WeaponContainer;
    public GameObject GameOverScreen;


    public void UpdateHealth(float value)
    {
        float health_value = value / 100;
        Health_bar.fillAmount = health_value;

    }

    public void  UpdateStamina(float value)
    {
        float stamina_value = value / 100;
        Stamina_Bar.fillAmount = stamina_value;
    }

    public void DisableWeapons()
    {
        if(WeaponContainer !=null) { 
            WeaponContainer.SetActive(false);
            GameOverScreen.SetActive(true);
            GameOverScreen.GetComponent<UI_Manager>().InitGameOverScreen();
            GetComponent<PlayerAttack>().Cross_Hair_Image.SetActive(false);


        }
    }



}
