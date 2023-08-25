using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FieldObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<FieldObjectMovement> _objectPrefabs = new List<FieldObjectMovement>();

    [SerializeField] private FieldObjectMovement[] activePrefab = new FieldObjectMovement[10];

    [Header("UI")]
    [SerializeField] private TMP_Text _lenghtText;
    [SerializeField] private GameObject _gameoverPanel;
    [SerializeField] private TMP_Text _gameoverText;

    private void Start()
    {
        activePrefab[5] = Instantiate(_objectPrefabs[0], transform.position, Quaternion.identity);
        activePrefab[5].Setup(this, _player.transform, 5, 1000);

        for (int i = 0; i < 10; i++)
        {
            if (i == 5) continue;

            activePrefab[i] = Instantiate(_objectPrefabs[Random.Range(0, _objectPrefabs.Count)], transform.position + Vector3.forward * (i - 5) * 200, Quaternion.identity);
            activePrefab[i].Setup(this, _player.transform, i, 1000);
        }
    }

    public void ActiveNewLoad(int index)
    {
        Vector3 pos = activePrefab[index].gameObject.transform.position;
        Destroy(activePrefab[index].gameObject);
        activePrefab[index] = Instantiate(_objectPrefabs[Random.Range(0, _objectPrefabs.Count)], pos + Vector3.forward * 2000, Quaternion.identity);
        activePrefab[index].Setup(this, _player.transform, index, 600);
    }

    private void Update()
    {
        _lenghtText.text = _player.transform.position.z.ToString("0.0");

        if(_player.transform.position.y <= -100)
        {
            _gameoverPanel.SetActive(true);
            _gameoverText.text = "지난 거리 :" + _player.transform.position.z.ToString("0.0");
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
