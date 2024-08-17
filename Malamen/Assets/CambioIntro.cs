using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioIntro : MonoBehaviour
{
    public GameObject[] pantallas;
    public int pantalla;
    public MenuInicial menu;

    private void Start()
    {
        menu = FindAnyObjectByType<MenuInicial>();
    }

    public void Cambio()
    {
        pantalla += 1;
        for (int i = 0; i < pantallas.Length; i++)
        {
            if ( i == pantalla)
            {
                pantallas[i].SetActive(true);
            }
            else
            {
                pantallas[i].SetActive(false);
                menu.Jugar();
            }
        }
    }
}
