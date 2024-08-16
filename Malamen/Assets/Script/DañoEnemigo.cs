using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoEnemigo : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<VidaJugador>().TomarDaño(1, other.GetContact(0).normal);
        }
    }
}
