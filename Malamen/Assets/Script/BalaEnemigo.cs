using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    public float velocidad;
    public int daño;
    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out VidaJugador vidaJugador))
        {
            vidaJugador.TomarDaño(daño);
        }
    }

}
