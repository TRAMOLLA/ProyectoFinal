using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private SpriteRenderer spriteRenderer;

    public bool sePuedeMover = true;

    [SerializeField] private Vector2 velocidadRebote;
    private Vector2 input;

    [Header("Movimiento")]

    private float movimientoHorizontal = 0f;

    [SerializeField] private float velocidadDeMovimiento;

    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento;

    private Vector3 velocidad = Vector3.zero;

    private bool mirandoDerecha = true;

    [Header("Salto")]

    [SerializeField] private float fuerzaDeSalto;

    [SerializeField] private LayerMask queEsSuelo;

    [SerializeField] private Transform controladorSuelo;

    [SerializeField] private Vector3 dimensionesCaja;

    [SerializeField] private bool enSuelo;

    private bool salto = false;

    [SerializeField] private int saltosExtraRestantes;
    [SerializeField] private int saltosExtra;

    [Header("Animacion")]
    private Animator animator;

    [Header("Dash")]
    [SerializeField] private float velocidadDash;
    [SerializeField] private float tiempoDash;
    [SerializeField] private TrailRenderer trailRenderer;
    private float gravedadInicial;
    private bool puedeHacerDash = true;

    [Header("Escalar")]
    [SerializeField] private float velocidadEscalar;
    private BoxCollider2D boxCollider2D;
    private bool escalando;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        gravedadInicial = rb2D.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);

        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

        if (Input.GetButtonDown("Jump")) {
            salto = true;
        }
        if (Input.GetKeyDown(KeyCode.B) && puedeHacerDash)
        {
            StartCoroutine(Dash());
        }
        if (enSuelo)
        {
            saltosExtraRestantes = saltosExtra;
        }
        if (Mathf.Abs(rb2D.velocity.y) > Mathf.Epsilon)
        {
            animator.SetFloat("Velocidad",Mathf.Sign(rb2D.velocity.y)); 
        }
        else
        {
            animator.SetFloat("Velocidad", 0);
        }
    }

    private void FixedUpdate()
    {
        Movimiento(salto);
        animator.SetBool("enSuelo",enSuelo);
        //Mover
        if (sePuedeMover)
        {
            Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        }
        salto = false;
        Escalar();
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            //Girar
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            //Girar
        }

        if (enSuelo && saltar) {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }

    }

    private void Salto()
    {
        //rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        rb2D.velocity = new Vector2(0f, fuerzaDeSalto);
        salto = false;
    }

    private void Movimiento(bool salto)
    {
        if (salto)
        {
            if (enSuelo)
            {
                Salto();
            }
        }
        else
        {
            if (salto && saltosExtraRestantes < 0)
            {
                Salto();
                saltosExtraRestantes -= 1;
            }
        }
    }

    public void Rebote(Vector2 puntoGolpe)
    {
        rb2D.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        //Girar
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

    }

     private void Escalar()
    {
        if ((input.y != 0 || escalando) && (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Escaleras"))))
        { 
            Vector2 velocidadSubida = new Vector2(rb2D.velocity.x, input.y * velocidadEscalar);
            rb2D.velocity = velocidadSubida;
            rb2D.gravityScale = 0;
            escalando = true; 
        }
        else
        {
            rb2D.gravityScale = gravedadInicial;
            escalando = false;
            if (enSuelo)
            {
                escalando = false;
            }
        }
        animator.SetBool("estaEscalando", escalando);
    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
    private IEnumerator Dash()
    {
        sePuedeMover = false;
        puedeHacerDash = false;
        rb2D.gravityScale = 0;
        rb2D.velocity = new Vector2(velocidadDash * transform.localScale.x, 0);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(tiempoDash);
        sePuedeMover = true;
        puedeHacerDash = true;
        rb2D.gravityScale = gravedadInicial;
        trailRenderer.emitting = false;
        animator.SetTrigger("Dash");
    }
}
