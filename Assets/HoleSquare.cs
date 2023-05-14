using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSquare : MonoBehaviour
{
    [SerializeField] GameObject flag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // move the flag to up position + 5
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y + 5, flag.transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // move the flag to down position - 5
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y - 5, flag.transform.position.z);
        }
    }
}
