using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Transform _camera;
    private Vector3 _inputVec;
    private Vector3 _moveVec;
    private Rigidbody _rb;

    [SerializeField] private bool _isRun;
    [SerializeField] private float _curSpeed;
    [SerializeField] private float _curAcc;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _moveAcc;
    [SerializeField] private float _runAcc;
    [SerializeField] private float _jumpForce;

    private void Start()
    {
        _camera = Camera.main.transform;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SpeedContorll();
    }
    private void FixedUpdate()
    {
        _rb.AddForce(_moveVec, ForceMode.Impulse);
    }

    private void SpeedContorll()
    {
        if (_isRun)
        {
            _curSpeed += _runSpeed * _runAcc * Time.deltaTime;
            _curSpeed = _curSpeed > _runSpeed ? _runSpeed : _curSpeed;
        }
        else
        {
            _curSpeed += _moveSpeed * _moveAcc * Time.deltaTime;
            _curSpeed = _moveAcc > _moveSpeed ? _moveSpeed : _moveAcc;
        }
        _moveVec = _inputVec * _curSpeed;


    }


    public void OnJump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
    public void OnMove(InputValue inputValue)
    {
        if (inputValue == null)
        {
            _inputVec = Vector3.zero;
            return;
        }
        Vector2 input = inputValue.Get<Vector2>().normalized;

        _inputVec = new Vector3(input.x, 0, input.y);
    }

    public void OnRun(InputValue inputValue)
    {
        if (inputValue == null) return;

        _isRun = inputValue.isPressed;
    }
}
