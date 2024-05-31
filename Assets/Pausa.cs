using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    private int SceneIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void ResumeGame()
    {
        // Obtén el objeto PauseManager de la escena principal
        PauseManager pauseManager = FindObjectOfType<PauseManager>();

        if (pauseManager != null)
        {
            pauseManager.ResumeGame();
        }
        else
        {
            Debug.LogError("No se encontró el script PauseManager en la escena principal.");
        }
    }

    public void Reiniciar()
    {
        Debug.Log("Reiniciar...");

        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        // Cambiar a la escena de título
        SceneManager.LoadScene(SceneIndex, LoadSceneMode.Single);
    }


}
