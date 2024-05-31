using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovCamera : MonoBehaviour
{
    // Referencia a la Cinemachine Virtual Camera
    public CinemachineVirtualCamera virtualCamera;

    // Velocidad de rotación de la cámara
    public float rotationSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener la entrada de rotación horizontal
        float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // Rotar la Cinemachine Virtual Camera alrededor del objetivo (personaje)
        RotateCameraAroundTarget(horizontalRotation);
    }

     // Método para rotar la cámara alrededor del objetivo
    void RotateCameraAroundTarget(float rotation)
    {
         AxisState xAxis = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis;

        // Aplicar la rotación al eje X de la cámara
        xAxis.m_InputAxisValue = rotation;

    }
}
