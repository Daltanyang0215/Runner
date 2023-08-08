using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRopeLauncher : MonoBehaviour
{
    [SerializeField] private Transform _ropePoint;
    [SerializeField] private float _ropeDistance;
    [SerializeField] private float _ropeSpeed;
    [SerializeField] private float _ropeWidth;

    private SpringJoint _springJoint;
    private RaycastHit _hit;
    private Camera _camera;
    private Ray CamaraRay => _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

    private void Start()
    {
        _camera = Camera.main;
        _springJoint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            

        }
        else // 마우스가 안눌린 상태에서 로프 포인트 확인
        {
            if (Physics.Raycast(CamaraRay, out _hit, _ropeDistance, 1 << LayerMask.NameToLayer("Anchor")) ||
                Physics.SphereCast(CamaraRay, _ropeWidth, out _hit, _ropeDistance, 1 << LayerMask.NameToLayer("Anchor")))
            {
                _ropePoint.gameObject.SetActive(true);

                _ropePoint.transform.position = _hit.point + _hit.normal * 0.01f;
                _ropePoint.transform.forward = -_hit.normal; // quad 라 벡터가 뒤집어진 상태임

                if(Mouse.current.leftButton.wasPressedThisFrame)
                {
                    _springJoint.connectedAnchor = _hit.point;// - transform.position;
                    _springJoint.maxDistance = _ropeDistance;

                }
            }
            else
            {
                _ropePoint.gameObject.SetActive(false);
            }
        }

    }
}
