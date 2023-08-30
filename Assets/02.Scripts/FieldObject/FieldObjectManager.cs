using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FieldObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private FieldObjectMovement[] activePrefab = new FieldObjectMovement[10];

    [Header("UI")]
    [SerializeField] private TMP_Text _lenghtText;
    [SerializeField] private GameObject _gameoverPanel;
    [SerializeField] private TMP_Text _gameoverText;

    private int randomrangeMin;
    private int randomrangeMax;

    private void Start()
    {
        randomrangeMin = 0;
        randomrangeMax = ObjectPool.Instance.elements.Count/2;

        activePrefab[5] = ObjectPool.Instance.Spwan($"Building0",
                                                       transform.position ,
                                                       Quaternion.identity,
                                                       transform).GetComponent<FieldObjectMovement>();
        activePrefab[5].Setup(this, _player.transform, 5, 1000);

        for (int i = 0; i < 10; i++)
        {
            if (i == 5) continue;

            activePrefab[i] = ObjectPool.Instance.Spwan($"Building{Random.Range(randomrangeMin, randomrangeMax)}",
                                                       transform.position + Vector3.forward * (i - 5) * 200,
                                                       Quaternion.identity,
                                                       transform).GetComponent<FieldObjectMovement>();
            activePrefab[i].Setup(this, _player.transform, i, 1000);
        }
    }

    public void ActiveNewLoad(int index)
    {
        Vector3 pos = activePrefab[index].gameObject.transform.position;
        ObjectPool.Instance.PoolRetrun(activePrefab[index].gameObject);
        activePrefab[index] = ObjectPool.Instance.Spwan($"Building{Random.Range(randomrangeMin, randomrangeMax)}",
                                                        pos + Vector3.forward * 2000,
                                                        Quaternion.identity,
                                                        transform).GetComponent<FieldObjectMovement>();
        activePrefab[index].Setup(this, _player.transform, index, 600);
    }

    private void Update()
    {
        _lenghtText.text = _player.transform.position.z.ToString("0.0");

        randomrangeMin = (int)(_player.transform.position.z / 1000f);
        randomrangeMin = randomrangeMin > ObjectPool.Instance.elements.Count / 2 ? ObjectPool.Instance.elements.Count / 2 : randomrangeMin;
        randomrangeMax = (int)(_player.transform.position.z / 1000f)+ ObjectPool.Instance.elements.Count / 2;
        randomrangeMax = randomrangeMax > ObjectPool.Instance.elements.Count ? ObjectPool.Instance.elements.Count : randomrangeMax;

        if (_player.transform.position.y <= -100)
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
