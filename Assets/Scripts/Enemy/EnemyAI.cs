using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent naveMesh;
    float visibleDistance=10;
    [SerializeField]Transform targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        naveMesh=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,targetPosition.position)<=visibleDistance){
            Chase();
        }
    }

  
    void Chase(){
        naveMesh.SetDestination(targetPosition.position);
        if(Vector3.Distance(transform.position,targetPosition.position)<=naveMesh.stoppingDistance)
            Attack();
    }
    void Attack(){
        Debug.Log(name + "Is Attacking the player");
    }
       void OnDrawGizmos()
    {
        
        Gizmos.DrawWireSphere(transform.position,visibleDistance);
        
    }
}
