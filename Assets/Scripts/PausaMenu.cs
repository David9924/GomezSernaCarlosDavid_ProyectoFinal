using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    private int pauseSceneIndex = 2;

    // Actualiza una vez por frame
    void Update()
    {
        // Detectar si se presiona la tecla de pausa (por ejemplo, Escape)
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
        // Cambiar a la escena de pausa
        SceneManager.LoadScene(pauseSceneIndex, LoadSceneMode.Additive);
        Time.timeScale = 0f; // Pausa el tiempo del juego
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