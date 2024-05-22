using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
public  class AttackScript : MonoBehaviour
    {

        public float damage = 10f;
        public float radius = 1f;
        public LayerMask targetLayer;

        public void Update()
        {
            Collider[] hit_Objects=Physics.OverlapSphere(transform.position, radius,targetLayer);
            if(hit_Objects.Length > 0 )
            {
                HealthScript health_script = hit_Objects[0].GetComponent<HealthScript>();
                health_script.ApplyDamage(damage);
                this.gameObject.SetActive(false);
            }
        }
    }
}
