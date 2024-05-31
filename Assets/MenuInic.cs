using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Nombre de la escena del juego que se va a cargar
    public string gameSceneName = "SampleScene";

    // Este método se llama al inicio del juego para cargar la escena principal
    void Start()
    {

    }

    // Método para cargar la escena del juego
    public void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // Método para pausar el juego
    public void QuitGame()
    {
        Application.Quit(); // Cierra el juego
        Debug.Log("El juego se ha cerrado.");

    }
}