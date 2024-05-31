using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public Transform Inicio; // Punto de activación de la misión
    public Transform destination; // Punto de destino al que el jugador debe llegar
    public float completionRadius = 2f; // Radio de proximidad para completar la misión
    public Canvas missionCanvas; // Referencia al canvas que muestra el objetivo de la misión
    public Canvas completionCanvas; // Referencia al canvas que muestra el mensaje de "completado"


    private bool missionActive = false;
    private bool missionCompleted = false;

    void Start()
    {
        // Se desactivan los Canvas
        missionCanvas.gameObject.SetActive(false);
        completionCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        // Si el jugador está lo suficientemente cerca del punto de activación, activa la misión
        if (!missionActive && !missionCompleted && Vector3.Distance(transform.position, Inicio.position) <= completionRadius)
        {
            StartMission();
        }

        // Si la misión está activa y el jugador está dentro del radio de completado, completa la misión
        if (missionActive && !missionCompleted && Vector3.Distance(transform.position, destination.position) <= completionRadius)
        {
            CompleteMission();
        }
    }

    void StartMission()
    {
        // Activa el canvas de la misión y muestra el mensaje
        missionActive = true;
        missionCanvas.gameObject.SetActive(true);


        // Después de un tiempo, oculta el canvas de la misión
        Invoke("HideMissionCanvas", 7f);
    }

    void CompleteMission()
    {
        // Muestra el canvas de completado y el mensaje
        missionCompleted = true;
        completionCanvas.gameObject.SetActive(true);

    }

    void HideMissionCanvas()
    {
        // Oculta el canvas de la misión después de mostrarlo por un tiempo
        missionCanvas.gameObject.SetActive(false);
    }
}