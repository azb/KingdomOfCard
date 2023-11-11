using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject stopObject;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == this.stopObject)
        {
            Destroy(gameObject);
        }
    }
}
