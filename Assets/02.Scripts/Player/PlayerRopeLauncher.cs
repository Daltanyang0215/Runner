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
    private LineRenderer _lineRenderer;
    private RaycastHit _hit;
    private Camera _camera;
    private Ray CamaraRay => _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

    private void Start()
    {
        _camera = Camera.main;
        _springJoint = GetComponent<SpringJoint>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void Update()
    {

        //if (Physics.Raycast(CamaraRay, out _hit, _ropeDistance, 1 << LayerMask.NameToLayer("Anchor")) ||
        //Physics.SphereCast(CamaraRay, _ropeWidth, out _hit, _ropeDistance, 1 << LayerMask.NameToLayer("Anchor")))

        if (Physics.Raycast(CamaraRay, out _hit, int.MaxValue, 1 << LayerMask.NameToLayer("Anchor")) ||
            Physics.SphereCast(CamaraRay, _ropeWidth, out _hit, int.MaxValue, 1 << LayerMask.NameToLayer("Anchor")))
        {
            Vector3 hitDir = _hit.point - transform.position + Vector3.up;
            if (Physics.Raycast(transform.position + Vector3.up, hitDir, out _hit, _ropeDistance, 1 << LayerMask.NameToLayer("Anchor")) ||
                Physics.SphereCast(transform.position + Vector3.up, _ropeWidth, hitDir, out _hit, _ropeDistance, 1 << LayerMask.NameToLayer("Anchor")))
            {
                _ropePoint.gameObject.SetActive(true);

                _ropePoint.transform.position = _hit.point + _hit.normal * 0.01f;
                _ropePoint.transform.forward = -_hit.normal; // quad 라 벡터가 뒤집어진 상태임

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    _lineRenderer.enabled = true;

                    _lineRenderer.SetPosition(0, _ropePoint.transform.position);
                    _lineRenderer.SetPosition(1, transform.position + Vector3.up);

                    _springJoint.connectedAnchor = _hit.point;// - transform.position;
                    _springJoint.maxDistance = Vector3.Distance(_hit.point, transform.position + Vector3.up);
                }
            }

        }
        else
        {
            _ropePoint.gameObject.SetActive(false);
        }

        if (Mouse.current.leftButton.isPressed)
        {
            _lineRenderer.SetPosition(1, transform.position + Vector3.up);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _lineRenderer.enabled = false;
            _springJoint.maxDistance = float.PositiveInfinity;
        }

    }
}
