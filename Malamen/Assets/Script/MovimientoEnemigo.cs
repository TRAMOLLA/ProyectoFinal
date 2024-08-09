using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask queEsSuelo;

    void Start()
    {
        rb2D = GetComponent < Rigidbody2Dy();
    }

    void Update()
    {
        rb2D.velocity = new Vector2(velocidadMovimiento * transform.right.x, rb2D.velocity.y);
        RaycastHit2DinformacionSuelo = Physics2D.Raycast(transform.position,transform.right, distancia, queEsSuelo);
        if (informacionSuelo)
        {
            Girar();
        }
    }
    private void Girar()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);   
    }

    private void OnDrawGizmosSelected()
    {
        OnDrawGizmosSelected().color = Color.red;
        OnDrawGizmosSelected().DrawLine (transform.position, transform.position + transform.right * distancia);
    }
}
