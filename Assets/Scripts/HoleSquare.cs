using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleSquare : MonoBehaviour
{
    [SerializeField] GameObject flag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            flag.transform.DOMoveY(flag.transform.position.y + 5, 1f);
            //flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y + 5, flag.transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            flag.transform.DOMoveY(flag.transform.position.y - 5, 1f);
            //flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y - 5, flag.transform.position.z);
        }
    }
}
