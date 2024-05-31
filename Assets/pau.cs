using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pau : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool isPaused = false;
    private int pauseSceneIndex = 2; // Índice de la escena de pausa en Build Settings

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        // Cargar la escena de pausa
        SceneManager.LoadScene(pauseSceneIndex, LoadSceneMode.Additive);
        Time.timeScale = 0f; // Pausar el tiempo del juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Descargar la escena de pausa
        SceneManager.UnloadSceneAsync(pauseSceneIndex);
        Time.timeScale = 1f; // Reanudar el tiempo del juego
        isPaused = false;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
