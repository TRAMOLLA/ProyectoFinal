using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJugador : MonoBehaviour;
{
    [Serialize Field] private float vida;

    private MovimientoJugador movimientoJugador;

    [Serialize Field] private float tiempoPerdidaControl;

    private Animator animator;

    private void Start()
    {
        movimientoJugador = GetComponent<MovimientoJugador>();
        animator = GetComponent<animator>();
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
    }

    public void TomarDaño(float daño, Vector2 posicion)
    {
        vida -= daño;
        animator.SetTrigger("Golpe");
        StartCoroutine(PerderControl());
        movimientoJugador.Rebote(posicion);
    }

    private IEnumerator PerderControl()
    {
        movimientoJugador.SePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoJugador.SePuedeMover = true;
    }

}
