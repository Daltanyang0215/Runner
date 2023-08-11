using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectMovement : MonoBehaviour
{
    private Transform _playerPos;
    private float _destroyDistance;
    [SerializeField] private Vector3 size = Vector3.one;

    public void Setup(Transform plyerpos, float distace)
    {
        _playerPos = plyerpos;
        _destroyDistance = distace;
    }

    private void Update()
    {
        if(_playerPos.position.z - transform.position.z > _destroyDistance)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
