using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] List<Transform> positions;

    int index;

    private void Start() {
        Move();
    }

    private void Move()
    {
        var pos = positions[index];
        this.transform.DOMove(pos.position, duration).SetEase(Ease.Linear).OnComplete(() => {
            index++;
            if (index >= positions.Count)
            {
                index = 0;
            }
            Move();
        });

    }
}
