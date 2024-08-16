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

    public void TomarDa�o(float da�o)
    {
        vida -= da�o;
    }

    public void TomarDa�o(float da�o, Vector2 posicion)
    {
        vida -= da�o;
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
