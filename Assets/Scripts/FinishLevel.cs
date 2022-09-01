using System.Collections;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject _prefabFX;
    
    private GameManager _gameManager;
    private Renderer _renderer;
    private int _transparency = Shader.PropertyToID("_Transparency");

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerMovement>())
            return;
        int numberOfCells = other.GetComponentInChildren<TailManagment>().NumberOfCells;
        _gameManager.LevelComplete(numberOfCells);
        Instantiate(_prefabFX, transform.position, Quaternion.identity);
        StartCoroutine(Dissolving(0.5f));
    }
    private IEnumerator Dissolving(float time)
    {
        for (float t = 0.8f; t > 0; t -= Time.deltaTime * time)
        {
            SetTransparency(t);
            yield return null;
        }
        Destroy(gameObject);
    }
    private void SetTransparency(float value)
    {
        _renderer.material.SetFloat(_transparency, value);
    }

}