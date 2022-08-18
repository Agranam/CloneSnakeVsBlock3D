using DG.Tweening;
using UnityEngine;

public class CellsRotation : MonoBehaviour
{
    [SerializeField] private float _duration = 1;
    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), _duration).
            SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetLink(gameObject);
    }
}
