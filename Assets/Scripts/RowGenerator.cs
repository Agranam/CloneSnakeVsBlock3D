using UnityEngine;

public class RowGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;


    private void Awake()
    {
        Generator();
    }

    private void Generator()
    {
        for (int i = 0; i < 6; i++)
        {
            Vector3 currentPosition = transform.position;
            GameObject block = Instantiate(_blockPrefab, currentPosition, Quaternion.identity);
            Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);
            block.transform.position = currentPosition + (Vector3.right * i * 3);
            Block createdBlock = block.GetComponent<Block>();
            createdBlock.SetBlockCount(Random.Range(1, 10));
        }
    }
}
