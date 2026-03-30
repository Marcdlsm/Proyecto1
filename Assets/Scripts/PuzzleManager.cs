using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Sprite cuadroGris;

    private TileControl[,] tablero = new TileControl[4, 4];
    private bool[,] estados = new bool[4, 4]; //false = gris, true = color

    void Start()
    {
        GenerarTablero();
    }

    public Sprite[] colorSprites; //Las 16 a color
    public Sprite[] grisesSprites;   //Las 16 grises

    void GenerarTablero()
    {
        int index = 0;
        for (int fila = 0; fila < 4; fila++)
        {
            for (int col = 0; col < 4; col++)
            {
                GameObject nuevaPieza = Instantiate(tilePrefab, this.transform);
                TileControl script = nuevaPieza.GetComponent<TileControl>();

                script.greySprite = grisesSprites[index];
                script.colorSprite = colorSprites[index];

                tablero[fila, col] = script;
                estados[fila, col] = false;
                script.UpdateVisual(false);

                index++;
            }
        }
    }

    //Botones
    //Columnas
    public void PulsarColumna(int c)
    {
        for (int f = 0; f < 4; f++)
        {
            estados[f, c] = !estados[f, c];
            tablero[f, c].UpdateVisual(estados[f, c]);
        }
        ComprobarVictoria();
    }

    //Filas
    public void PulsarFila(int f)
    {
        for (int c = 0; c < 4; c++)
        {
            estados[f, c] = !estados[f, c];
            tablero[f, c].UpdateVisual(estados[f, c]);
        }
        ComprobarVictoria();
    }

    void ComprobarVictoria()
    {
        foreach (bool esColor in estados)
        {
            if (!esColor) return; // Ganar si son a color tds
        }
        Debug.Log("¡LO LOGRASTE! Has restaurado el bosque.");
    }
}