using Assets.Scripts.Player.Weapons;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowAndSpearScript : MonoBehaviour
{
    // Start is called before the first frame update

    #region Variable Declarations

    Rigidbody Object_RigidBody;
    BoxCollider MyCollider;
    Camera Main_FpsCamera;
    public float speed = 1f;
    public float deactive_time = 3f;
    public float damage = 15f;

  
    #endregion


    private void Awake()
    {
        Object_RigidBody = GetComponent<Rigidbody>();
        MyCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
      Invoke("DisableArrow", deactive_time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == TagManager.ENEMY_TAG)
        {
            HealthScript health_object=other.GetComponent<HealthScript>();  
            health_object.ApplyDamage(damage);
        }
    }
    public void Launch(Camera FpsCamera)
    {
       
            Object_RigidBody.velocity = FpsCamera.transform.forward * speed;
           
            transform.LookAt(transform.position + Object_RigidBody.velocity);
            
           
        
      
    }

    void DisableArrow()
    {
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<BowAndSpearScript>().gameObject.SetActive(false);
      
        Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
