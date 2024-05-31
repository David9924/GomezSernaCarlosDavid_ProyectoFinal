using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript2 : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;

    public Quaternion angulo;

    public float grado;

    public GameObject target;
    public float detectionRadius = 5f;

    private NavMeshAgent agent;

    public int salud = 4; // Health of the zombie
    
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Idle"); // Asegúrate de que el nombre del jugador es correcto
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
    }

    public void Comportamiento()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > detectionRadius)
        {
            ani.SetBool("Correr", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("Caminar", false);
                    agent.isStopped = true;
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    agent.isStopped = false;
                    agent.destination = transform.position + transform.forward * 2f;
                    ani.SetBool("Caminar", true);
                    break;
            }
        }
        else
        {
            if (distance > 1f) // Si está cerca pero no en el radio de ataque
            {
                ani.SetBool("Caminar", false);
                ani.SetBool("Correr", true);

                agent.isStopped = false;
                agent.destination = target.transform.position;
            }
            else // Si está muy cerca del jugador
            {
                ani.SetBool("Caminar", false);
                ani.SetBool("Correr", false);

                agent.isStopped = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            PlayerController playerController = target.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ReceiveDamage();
            }
        }
    }

    // Llamado cuando el zombie recibe daño
    public void ReceiveDamage()
    {
        salud--;
        ani.SetTrigger("Recibedaño");

        if (salud <= 0)
        {
            Die();
        }
    }

    // Llamado cuando el zombie muere
    void Die()
    {
        ani.SetTrigger("Muere");
        // Desactivar el zombie después de la animación de muerte
        StartCoroutine(DestroyAfterAnimation());
    }


    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }



}