using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public Transform target;

    NavMeshAgent agent;

    public enum EnemyState
    {
        Chase, Patrol
    }

    public EnemyState currentState = EnemyState.Patrol;

    public GameObject[] path;
    public int pathIndex;
    public float distThreshhold;
    monsterSpawner Spawner;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (path.Length <= 0)
        {
            path = GameObject.FindGameObjectsWithTag("Patrol");
        }
        
        if (currentState == EnemyState.Chase)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;

            if (target)
                agent.SetDestination(target.position);
        }
    
        if (distThreshhold <= 0)
        {
            distThreshhold = 0.5f;
        }

    }

    // Update is called once per frame
    void Update()
    {
       if (currentState == EnemyState.Patrol)
        {
            if (target)
                Debug.DrawLine(transform.position, target.position, Color.red);

            if (agent.remainingDistance < distThreshhold)
            {
                pathIndex++;
                pathIndex %= path.Length;

                target = path[pathIndex].transform;
            }
        }

       if (currentState == EnemyState.Chase)
        {
            if (target.CompareTag("Patrol"))
                target = GameObject.FindWithTag("Player").transform;
        }

        if (target)
            agent.SetDestination(target.position);


    }

   


    public void SetSpawner(monsterSpawner _spawner)
    {
        Spawner = _spawner;
    }


}
