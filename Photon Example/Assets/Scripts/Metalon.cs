using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    MOVE,
    ATTACK,
    DIE
}

public class Metalon : MonoBehaviour
{
    Animator animator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] Transform turretTrans;
    [SerializeField] State state;
    private int health;
    


    public int Health
    {
        set { health = value; }
        get { return health; }
    }
    void Start()
    {
        health = 100;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        turretTrans = GameObject.Find("Turret Tower").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.MOVE:
                Move();
                break;
            case State.ATTACK:
                Attack(); 
                break;
            case State.DIE:
                Die(); 
                break;
        }
    }
    public void Attack()
    {
        animator.SetBool("Attack", true);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Turret Tower"))
        {
            state = State.ATTACK;
        }
    }

    public void Die()
    {
        animator.Play("Die");
    }

    public void Move()
    {
        navMeshAgent.SetDestination(turretTrans.position);
    }
}
