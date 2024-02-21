using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Metalon : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] Transform turretTrans;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        turretTrans = GameObject.Find("Turret Tower").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    public void Move()
    {
        navMeshAgent.SetDestination(turretTrans.position);
    }
}
