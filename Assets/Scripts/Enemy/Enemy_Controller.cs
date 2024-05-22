using Assets.Scripts.Enemy;
using Assets.Scripts.Player.Weapons;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;



public enum ENEMY_STATE
{
    PATROL,
    CHASE,
    ATTACK,
    DEAD
}
public class Enemy_Controller : MonoBehaviour
{
    // Start is called before the first frame update


    #region Variable Declarations

    private Enemy_Animator _Animator;
    public ENEMY_STATE CurrentEnemeyState;

    public float chase_distance = 7f;
    private float current_Chase_Distance;
    private float attack_distance = 1.8f;
    public float chase_after_attack_distance = 2f;
    public float Gravity = 20f;
    public float vertical_veloctiy;
    public Transform Target;
    public float run_speed = 4f;
    public float walk_speed = 0.5f;
    public NavMeshAgent Nav_Mesh_Agent;

    public float Random_Radious_min = 20f, Random_Radious_max = 60f;
    public float AttackTimer;
    public float Wait_Before_Attack = 2f;
    public float Patrol_Timer;
    public float Patrol_Timer_For_This_Time = 15f;

    public GameObject Attack_Point;

    public Enemy_Audio _enemyAudio;

    public HealthScript health_Object;
    #endregion


    private void Awake()
    {
        _Animator = GetComponent<Enemy_Animator>();
        Target = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        CurrentEnemeyState = ENEMY_STATE.PATROL;
        Nav_Mesh_Agent = GetComponent<NavMeshAgent>();
        _enemyAudio=GetComponentInChildren<Enemy_Audio>();
        health_Object = GetComponent<HealthScript>();
    }
    private void Start()
    {
        Patrol_Timer = Patrol_Timer_For_This_Time;
        AttackTimer = Wait_Before_Attack;
        current_Chase_Distance = chase_distance;
    }

    void SetNewRandomDestination()
    {
        float random_radious = Random.Range(Random_Radious_min, Random_Radious_max);
        Vector3 Random_Direction = Random.insideUnitSphere * random_radious;
        Random_Direction += transform.position;
        NavMeshHit _navMeshHit;
        NavMesh.SamplePosition(Random_Direction, out _navMeshHit, random_radious, -1);
        Nav_Mesh_Agent.SetDestination(_navMeshHit.position);
    }
    public void ApplyGravity()
    {

        if (!Nav_Mesh_Agent.isOnNavMesh)
        {
            vertical_veloctiy -= Gravity * Time.deltaTime;

        }

        Nav_Mesh_Agent.velocity.Set(Nav_Mesh_Agent.velocity.x, vertical_veloctiy, Nav_Mesh_Agent.velocity.z);



    }
    private void Patrol()
    {
        if (Nav_Mesh_Agent.isStopped == true)
        {
            Nav_Mesh_Agent.isStopped = false;
        }
       
        Nav_Mesh_Agent.speed = walk_speed;

        Patrol_Timer += Time.deltaTime;
        if (Patrol_Timer > Patrol_Timer_For_This_Time)
        {
            SetNewRandomDestination();
            Patrol_Timer = 0f;
        }


        if (Nav_Mesh_Agent.velocity.sqrMagnitude > 0)
        {
            _Animator.Walk(true);
        }
        else
        {
            _Animator.Walk(false);
        }

        if (Vector3.Distance(transform.position, Target.position) <= chase_distance)
        {
            _Animator.Walk(false);
            CurrentEnemeyState = ENEMY_STATE.CHASE;
            _enemyAudio.PlayScreamSound();

        }
    }

    public void TurnOnAttackPoint()
    {
        Attack_Point.SetActive(true);
    }

    public void TurnOffAttackPoint()
    {
        if (Attack_Point.activeInHierarchy)
        {
            Attack_Point.SetActive(false);
        }
    }


    private void Chase()
    {
        Nav_Mesh_Agent.isStopped = false;
        Nav_Mesh_Agent.speed = run_speed;
        Nav_Mesh_Agent.SetDestination(Target.position);
        if (Nav_Mesh_Agent.velocity.sqrMagnitude > 0)
        {
            _Animator.Run(true);
        }
        else
        {
            _Animator.Run(false);
        }

        if (Vector3.Distance(transform.position, Target.position) <= attack_distance)
        {
            _Animator.Run(false);
            _Animator.Walk(false);
            CurrentEnemeyState = ENEMY_STATE.ATTACK;
            if (chase_distance != current_Chase_Distance)
            {
                chase_distance = current_Chase_Distance;
            }
        } else if (Vector3.Distance(transform.position, Target.position) > chase_distance)
        {
            _Animator.Run(false);
            _Animator.Walk(false);

            CurrentEnemeyState = ENEMY_STATE.PATROL;
            Patrol_Timer = Patrol_Timer_For_This_Time;
            if (chase_distance != current_Chase_Distance)
            {
                chase_distance = current_Chase_Distance;
            }
        }
    }

    private void Attack()
    {
        Nav_Mesh_Agent.velocity = Vector3.zero;
        Nav_Mesh_Agent.isStopped = true;
        AttackTimer += Time.deltaTime;
        if (AttackTimer >= Wait_Before_Attack)
        {
            _Animator.Attack();
            _enemyAudio.PlayAttackSound();
            AttackTimer = 0f;
        }

        if (Vector3.Distance(transform.position, Target.position) > attack_distance)
        {
            CurrentEnemeyState = ENEMY_STATE.CHASE;
        }

    }

    public void Enemy_Death()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (!health_Object.isDead)
        {
            ApplyGravity();

            if (CurrentEnemeyState == ENEMY_STATE.PATROL)
            {
                Patrol();

            }
            else if (CurrentEnemeyState == ENEMY_STATE.CHASE)
            {
                Chase();

            }
            else if (CurrentEnemeyState == ENEMY_STATE.ATTACK)
            {
                Attack();
            }
            else
            {

            }
        }
      
       
    }
}
