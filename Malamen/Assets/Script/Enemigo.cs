using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void TomarDa�o(float da�o)
    {
        vida -= da�o;

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {

    }
}
