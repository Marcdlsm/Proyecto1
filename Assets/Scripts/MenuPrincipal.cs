using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        
        SceneManager.LoadScene("EscenaPuzzle");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Cierra el juego (no funciona en Unity)
    }

 
    public void AbrirOpciones() //Falta logica
    {
        Debug.Log("Abriendo opciones...");
    }
}