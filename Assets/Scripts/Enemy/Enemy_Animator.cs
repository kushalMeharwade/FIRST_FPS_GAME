using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Animator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _Animator;

    private void Awake()
    {
         _Animator = GetComponent<Animator>();
    }


    public void Run(bool isRunning)
    {
       _Animator.SetBool(TagManager.ENEMEY_RUN, isRunning);
    }

    public void Walk(bool isWalking)
    {
        _Animator.SetBool(TagManager.ENEMY_WALK, isWalking);
    }

    public void Attack()
    {
        _Animator.SetTrigger(TagManager.ENEMY_ATTACK);
    }

    public void hasBeenHit()
    {
        _Animator.SetTrigger(TagManager.ENEMY_HIT_TRIGGER);
    }

    public void EnemyDied(bool isDead)
    {
        _Animator.SetBool(TagManager.ENEMY_DEAD_TRIGGER, isDead);
        _Animator.SetBool(TagManager.ENEMY_WALK, false);
    }
}
