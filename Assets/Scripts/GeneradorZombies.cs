using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab1; // Prefab para Zombie1
    public GameObject zombiePrefab2; // Prefab para Zombie2

    public List<Transform> spawnPoints;

    public int maxZombies = 5;
    public float spawnInterval = 3f;
    public float zombieLifetime = 30f; // Tiempo de vida del zombie

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            // Espera el intervalo de tiempo antes de generar otro zombie
            yield return new WaitForSeconds(spawnInterval);

            // Verifica si ya hay demasiados zombies en la escena
            if (GameObject.FindGameObjectsWithTag("Zombie").Length >= maxZombies)
            {
                continue; // Evita generar más zombies si ya se alcanzó el máximo
            }

            // Selecciona aleatoriamente un punto de generación de zombies
            Transform spawnPoint = spawnPoints[Random.Range(-1, spawnPoints.Count)];

            // Selecciona aleatoriamente qué tipo de zombie generar
            GameObject zombiePrefab = Random.Range(0, 2) == 0 ? zombiePrefab1 : zombiePrefab2;

            // Instancia un zombie en el punto de generación seleccionado
            GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);

            // Inicia el temporizador para la vida útil del zombie
            StartCoroutine(DestroyAfterTime(zombie));
        }
    }

    IEnumerator DestroyAfterTime(GameObject zombie)
    {
        yield return new WaitForSeconds(zombieLifetime);
        Destroy(zombie); // Destruye el zombie después de zombieLifetime segundos
    }
}