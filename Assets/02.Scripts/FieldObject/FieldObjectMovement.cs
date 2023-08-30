using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectMovement : MonoBehaviour
{
    private FieldObjectManager _manager;
    private Transform _playerPos;
    private float _destroyDistance;
    private int _index;

    public void Setup(FieldObjectManager manager, Transform plyerpos, int index, float distace)
    {
        _manager = manager;
        _playerPos = plyerpos;
        _index = index;
        _destroyDistance = distace;
    }

    private void LateUpdate()
    {
        if (_playerPos.position.z - transform.position.z > _destroyDistance)
        {
            _manager.ActiveNewLoad(_index);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
