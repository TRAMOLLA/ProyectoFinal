using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pantallaIni;
    public GameObject pantallaFin;
    public TMP_Text textoFinal;

    private void Awake()
    {
        pantallaFin.SetActive(false);
        Time.timeScale = 0;
    }

    public void IniciarJuego()
    {
        pantallaIni.SetActive(false);
        Time.timeScale = 1;
    }

    public void FinDeLuego()
    {
        pantallaFin.SetActive(true);
        time.timeScale = 0;
    }

    public void JuegoNuevo()
    {
        SceneManager.LoadScene(0);
    }

}
