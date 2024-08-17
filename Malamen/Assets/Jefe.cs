using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    private Animator animator;

    public Rigidbody2D rb2D;

    public Transform jugador;

    private bool mirandoDerecha = true;

    [Header("Vida")]

    [SerializeField] private float vida;

    [SerializeField] private BarraDeVida barraDeVida;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    public void TomarDaño(float daño)
    {
        vida -= daño;

        barraDeVida.CambiarVidaActual(vida);

        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }

    public void MirarJugador()
    {
        if ((jugador.position.x > transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

}
