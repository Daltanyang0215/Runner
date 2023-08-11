using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<FieldObjectMovement> _objectPrefabs = new List<FieldObjectMovement>();

    [SerializeField] private FieldObjectMovement[] activePrefab = new FieldObjectMovement[10];

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            activePrefab[i] = Instantiate(_objectPrefabs[Random.Range(0, _objectPrefabs.Count)], transform.position + Vector3.forward * i * 400, Quaternion.identity);
        }
    }
}
