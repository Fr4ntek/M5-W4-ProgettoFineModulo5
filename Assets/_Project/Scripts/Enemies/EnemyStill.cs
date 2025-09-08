using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStill : MonoBehaviour
{
    [SerializeField] private float _rotationStep = 90f;    
    [SerializeField] private float _waitTime = 2f;         
    [SerializeField] private float _rotationSpeed = 90f; 

    private void Start()
    {
        StartCoroutine(RotateEveryFewSecondsSmooth());
    }

    private IEnumerator RotateEveryFewSecondsSmooth()
    {
        while (true)
        {
            yield return new WaitForSeconds(_waitTime);

            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, _rotationStep, 0f);

            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

                yield return null;
            }

            transform.rotation = targetRotation;
        }
    }
}
