using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    Animator animator;
    public Rigidbody[] rigid;
    public int health;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeAwayHealth(int TakeAway)
    {
        health -= TakeAway;

        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        foreach (Rigidbody rb in rigid)
        {
            rb.isKinematic = false;
        }

        animator.enabled = false;
    }
}