using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        // Usamos el nombre entre comillas. 
        // Asegúrate de que se escriba EXACTO como en tu carpeta de Assets.
        SceneManager.LoadScene("EscenaPuzzle");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego..."); // Solo se verá en el editor
        Application.Quit(); // Cierra el juego (solo funciona en el build final)
    }

    // El botón de Opciones suele abrir otro panel, pero por ahora lo dejamos listo
    public void AbrirOpciones()
    {
        Debug.Log("Abriendo opciones...");
    }
}