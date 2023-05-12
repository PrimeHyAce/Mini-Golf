using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTile : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject tile;

    // Update is called once per frame
    void Update()
    {
        tile.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
