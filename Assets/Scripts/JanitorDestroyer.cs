using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }
    }
}
