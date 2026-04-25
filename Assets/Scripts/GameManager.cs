using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton para fácil acceso

    public int basuraTotal = 0;
    public int basuraRecogida = 0;
    public bool faseLimpiezaCompletada = false;

    // Lista para guardar todas las casillas y cambiarlas de golpe
    private Tile[] todasLasCasillas;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Buscamos todas las casillas en la escena al empezar
        todasLasCasillas = Object.FindObjectsByType<Tile>(FindObjectsSortMode.None);
    }

    public void RegistrarBasura()
    {
        basuraTotal++;
    }

    public void RecogerBasura()
    {
        basuraRecogida++;

        // Comprobar si hemos terminado
        if (basuraRecogida >= basuraTotal)
        {
            CompletarFaseLimpieza();
        }
    }

    void CompletarFaseLimpieza()
    {
        faseLimpiezaCompletada = true;
        Debug.Log("¡Fase de limpieza completada! Ahora a colorear.");

        // Opcional: Cambiar el color de todas las casillas para avisar
        foreach (Tile tile in todasLasCasillas)
        {
            tile.PrepararParaColorear();
        }
    }
}