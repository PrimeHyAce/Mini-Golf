using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Goal!");
        // if (other.TryGetComponent<Ball>(out Ball ball))
        // {
        //     ball.Goal();
        // }
    }
}
