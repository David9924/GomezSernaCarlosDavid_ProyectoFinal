using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menupaus : MonoBehaviour
{
    private int SceneIndex = 0;

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

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void Titulo()
    {
        // Cambiar a la escena de pausa
        SceneManager.LoadScene(SceneIndex, LoadSceneMode.Additive);

    }
}
