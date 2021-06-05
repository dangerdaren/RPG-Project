using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            MoveToCurser();
        }
        else
        {
            _navMeshAgent.destination = transform.position;
        }

        UpdateAnimator();

    }

    private void MoveToCurser()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        bool _hasHit = Physics.Raycast(_ray, out _hit);

        if (_hasHit)
        {
            _navMeshAgent.destination = _hit.point;
        }
    }

    private void UpdateAnimator()
    {
        Vector3 _velocity = _navMeshAgent.velocity;
        Vector3 _localVelocity = transform.InverseTransformDirection(_velocity);
        float speed = _localVelocity.z;
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }
}
