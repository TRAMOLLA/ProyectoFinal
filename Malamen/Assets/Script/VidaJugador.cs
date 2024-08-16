using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    [SerializeField] private float vida;

    private MovimientoJugador movimientoJugador;

    [SerializeField] private float tiempoPerdidaControl;

    private Animator animator;

    private void Start()
    {
        movimientoJugador = GetComponent<MovimientoJugador>();
        animator = GetComponent<Animator>();
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
        movimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoJugador.sePuedeMover = true;
    }

}
