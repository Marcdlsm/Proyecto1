using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int basuraTotal = 0;
    public int basuraRecogida = 0;
    public bool faseLimpiezaCompletada = false;

    private Tile[] todasLasCasillas;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        todasLasCasillas = Object.FindObjectsByType<Tile>(FindObjectsSortMode.None);
    }

    public void RegistrarBasura()
    {
        basuraTotal++;
    }

    public void RecogerBasura()
    {
        basuraRecogida++;

        if (basuraRecogida >= basuraTotal)
        {
            CompletarFaseLimpieza();
        }
    }

    void CompletarFaseLimpieza()
    {
        faseLimpiezaCompletada = true;
        Debug.Log("¡Fase de limpieza completada! Ahora a colorear.");

        foreach (Tile tile in todasLasCasillas)
        {
            tile.PrepararParaColorear();
        }
    }
}