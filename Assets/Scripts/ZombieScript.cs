using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;

    public Quaternion angulo;

    public float grado;

    public GameObject target;
    public bool Atacar;
    public float detectionRadius = 5f;
    public float attackRadius = 1f;

    private NavMeshAgent agent;

    public int salud = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Idle");
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

        if (distance > 5)
        {
            ani.SetBool("Correr", false);
            ani.SetBool("atacar", false);
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
          if (distance > attackRadius && !Atacar)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

                ani.SetBool("Caminar", false);
                ani.SetBool("Correr", true);
                ani.SetBool("atacar", false);

                agent.isStopped = false;
                agent.destination = target.transform.position;
            }
            else if (distance <= attackRadius)
            {
                ani.SetBool("Caminar", false);
                ani.SetBool("Correr", false);
                ani.SetBool("atacar", true);

                agent.isStopped = true;
                Atacar = true;
            }
        }

        
    }

    // Esta función se llamará desde el Animation Event
    public void DealDamage()
    {
        // Verifica que el target tenga un componente PlayerController
        PlayerController playerController = target.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ReceiveDamage();
        }
    }

       // Llamado cuando el zombie recibe daño
    public void ReceiveDamage()
    {
        salud--;
        ani.SetTrigger("RecibeDaño");

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
