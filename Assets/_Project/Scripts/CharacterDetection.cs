using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterDetection : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _target;
    [SerializeField] private float _viewAngle = 45f;
    [SerializeField] private float _sightDistance = 15f;
    [SerializeField] private LayerMask _whatIsObstacle;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _subdivisions = 12;

    private NavMeshAgent _agent;

    private void Start()
    {
        if(_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        _agent = GetComponent<NavMeshAgent>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        EvaluateConeOfView(_subdivisions);

    }
    void Update()
    {
        
        if (CanSeePlayer())
        {
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.red;
            _agent.SetDestination(_target.position);
        }
        else
        {
            _lineRenderer.startColor = Color.white;
            _lineRenderer.endColor = Color.white;
        }
    }

    public bool CanSeePlayer()
    {
        Vector3 toTarget = _target.position - transform.position;
        float sqrDistance = toTarget.sqrMagnitude;

        if (sqrDistance > _sightDistance * _sightDistance) 
            return false;

        float distance = Mathf.Sqrt(sqrDistance);
        toTarget /= distance;   
        
        if (Vector3.Dot(transform.forward, toTarget) < Mathf.Cos(_viewAngle * Mathf.Deg2Rad))
            return false;
        
        if (Physics.Linecast(_head.position, _target.position, _whatIsObstacle))
            return false;
    
        return true;
    }

    public void EvaluateConeOfView(int subdivisions)
    {
        float startAngle = (90 - _viewAngle) * Mathf.Deg2Rad;

        int totalPoints = subdivisions + 1;
        _lineRenderer.positionCount = totalPoints;

        Vector3[] arcPoints = new Vector3[totalPoints];

        float stepAngle = (2 * _viewAngle / subdivisions) * Mathf.Deg2Rad;

        for (int i = 0; i < subdivisions; i++)
        {
            float currentAngle = startAngle + i * stepAngle;

            arcPoints[i].x = Mathf.Cos(currentAngle) * _sightDistance;
            arcPoints[i].z = Mathf.Sin(currentAngle) * _sightDistance;
        }

        arcPoints[subdivisions] = Vector3.zero;

        _lineRenderer.SetPositions(arcPoints);

        // Cono di visione dinamico a seconda degli ostacoli - da correggere

        //int totalPoints = subdivisions + 1; 
        //_lineRenderer.positionCount = totalPoints;

        //float startAngle = - _viewAngle;

        //// Il punto di partenza del cono è sempre la posizione dell'oggetto
        //Vector3 lineOrigin = Vector3.zero;
        //Vector3 raycastOrigin = transform.position;
        //Vector3 forward = transform.forward;

        //_lineRenderer.SetPosition(0, lineOrigin);


        //float deltaAngle = _viewAngle / subdivisions;

        //for (int i = 0; i < subdivisions; i++)
        //{
        //    float currentAngle = startAngle + (2 * _viewAngle / subdivisions) + i;
        //    Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * forward;
        //    Vector3 point = lineOrigin + direction * _sightDistance;

        //    if (Physics.Raycast(raycastOrigin, direction, out RaycastHit hit, _sightDistance, _whatIsObstacle))
        //    {
        //        // Se colpisce un ostacolo, imposta il punto del cono sul punto di collisione
        //        point = hit.point - (raycastOrigin - lineOrigin);
        //    }

        //    _lineRenderer.SetPosition(i + 1, point);

        //}
    }

}
