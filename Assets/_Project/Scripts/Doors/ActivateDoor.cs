using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _uiPrompt;

    private bool _isPlayerInRange = false;
    private bool _isDoorVisible = true;
    private MeshRenderer _doorRenderer;
    private Collider _doorCollider;
    private NavMeshObstacle _doorObstacle;

    void Start()
    {
        if (_door == null) return;
        if (_uiPrompt != null) _uiPrompt.SetActive(false);

        _doorRenderer = _door.GetComponent<MeshRenderer>();
        _doorCollider = _door.GetComponent<Collider>();
        _doorObstacle = _door.GetComponent<NavMeshObstacle>();
    }

    void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (_door != null)
            {
                TogglePorta();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
            if (_uiPrompt != null) _uiPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
            if (_uiPrompt != null) _uiPrompt.SetActive(false);
        }
    }

    private void TogglePorta()
    {
        
        _isDoorVisible = !_isDoorVisible;
        _doorRenderer.enabled = _isDoorVisible;
        _doorCollider.enabled = _isDoorVisible;
        _doorObstacle.enabled = _isDoorVisible;
    }

}
