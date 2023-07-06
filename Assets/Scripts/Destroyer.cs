using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GreenNote") || collision.CompareTag("RedNote") || collision.CompareTag("YellowNote") || collision.CompareTag("BlueNote"))
        {
            Destroy(collision.gameObject);
        }
    }
}
