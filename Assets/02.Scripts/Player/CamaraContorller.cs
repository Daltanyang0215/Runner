using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamaraContorller : MonoBehaviour
{
    [SerializeField] private Transform _targetAnchor;
    [SerializeField] private Vector3 _cameraOffset;

    private void Start()
    {
        transform.position = _targetAnchor.position+ _cameraOffset;
        transform.LookAt(_targetAnchor.position);
    }

    private void Update()
    {
        transform.position = _targetAnchor.position + _cameraOffset;
    }


    public void OnLook(InputValue inputValue)
    {
        
    }
}
