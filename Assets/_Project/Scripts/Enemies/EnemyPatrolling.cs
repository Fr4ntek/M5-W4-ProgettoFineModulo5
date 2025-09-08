using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolling : MonoBehaviour
{
    private const float DestinationDistanceError = 0.01f;
    
    [SerializeField] private Transform[] _destinations;

    private int _destinationIndex;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetDestinationAtIndex(0);
    }

    void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            SetDestinationAtNextIndex();
        }
            
    }
    public void SetDestinationAtNextIndex() => SetDestinationAtIndex(_destinationIndex + 1);
    public void SetDestinationAtIndex(int index)
    {
        Debug.Log(index);
        if(index < 0)
        {
            while(index < 0)
            {
                index += _destinations.Length;
            }
        }
        else if (index >= _destinations.Length)
        {
            index = index % _destinations.Length;
        }

        _destinationIndex = index;
        _agent.SetDestination(_destinations[_destinationIndex].position);
    }

    //public bool IsCloseEnoughToDestination()
    //{
    //    float sqrStoppingDistance = _agent.stoppingDistance * _agent.stoppingDistance;
    //    Vector3 toDestination = _agent.destination - transform.position;
    //    float sqrDistance = toDestination.sqrMagnitude;
    //    if (sqrDistance <= sqrStoppingDistance + DestinationDistanceError)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}

