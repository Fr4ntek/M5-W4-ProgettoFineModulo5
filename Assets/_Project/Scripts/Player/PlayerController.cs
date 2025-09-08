using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _speedMultiplier = 2;

    private Rigidbody _rb;
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {

        // Movement
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        // Sprint
        _speed = Input.GetKey(KeyCode.LeftShift) ? _walkSpeed * _speedMultiplier : _walkSpeed;
        

    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(Horizontal, 0, Vertical).normalized;
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }
        Vector3 move = direction * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + move);
    }

}
