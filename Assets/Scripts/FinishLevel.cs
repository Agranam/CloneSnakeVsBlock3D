using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private GameManager _gameManager;
    private FinishLevel _finishLevel;

    private void Start()
    {
        _finishLevel = GetComponent<FinishLevel>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerMovement>())
            return;
        
        _gameManager.LevelComplete();
    }
}