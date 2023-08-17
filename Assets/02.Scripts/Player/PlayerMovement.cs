using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _inputVec;
    private Vector3 _moveVec;
    private Rigidbody _rb;

    [SerializeField] private bool _isRun;
    [SerializeField] private float _curSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _moveAcc;
    [SerializeField] private float _runAcc;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce;

    private void Start()
    {
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
        if (OnGoundCheck()) // ���϶��� �̵��� �԰�
        {
            _rb.drag = 3;
            if (_inputVec.z != 0)
            {
                if (_isRun)
                {
                    _curSpeed += _runSpeed * _runAcc * Time.deltaTime;
                    _curSpeed = _curSpeed > _runSpeed ? _runSpeed : _curSpeed;
                }
                else
                {
                    _curSpeed += _moveSpeed * _moveAcc * Time.deltaTime;
                    _curSpeed = _curSpeed > _moveSpeed ? _moveSpeed : _curSpeed;
                }
            }
            else
            {
                _curSpeed -= _moveSpeed * _moveAcc * Time.deltaTime;
                _curSpeed = _curSpeed < 0 ? 0 : _curSpeed;
            }


            //_moveVec = _inputVec * _curSpeed;
            //_moveVec = Quaternion.Euler(Vector3.up * _camera.eulerAngles.y) * _moveVec;
            _moveVec = Vector3.forward * _inputVec.z * _curSpeed;
            _moveVec = Quaternion.Euler(Vector3.up * transform.eulerAngles.y) * _moveVec;
            //_moveVec = Quaternion.Euler(Vector3.up * _curRotate) * _moveVec;
        }
        else
        {
            _rb.drag = 0;

            //_curSpeed = Mathf.Lerp(_curSpeed, 0, Time.deltaTime);
            //_moveVec = _moveVec.normalized * _curSpeed;
            _moveVec = Vector3.zero;
        }

        if (_inputVec.x != 0)
        {
            transform.Rotate(Vector3.up, _inputVec.x * Time.deltaTime * _rotateSpeed);
        }
    }

    private bool OnGoundCheck()
    {
        return Physics.CheckBox(transform.position + Vector3.up * 0.6f, Vector3.one * 0.7f, Quaternion.identity, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void OnJump()
    {
        if (OnGoundCheck())
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
    public void OnMove(InputValue inputValue)
    {
        if (inputValue == null)
        {
            _inputVec = Vector3.zero;
            return;
        }
        Vector2 input = inputValue.Get<Vector2>();

        _inputVec = new Vector3(input.x, 0, input.y);
    }
    public void OnRun(InputValue inputValue)
    {
        if (inputValue == null) return;

        _isRun = inputValue.isPressed;
    }
}
