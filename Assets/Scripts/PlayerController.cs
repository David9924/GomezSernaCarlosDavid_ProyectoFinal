using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;

    public List<GameObject> armas;

    private int armaSeleccionada = -1;
    public GameObject Arma;

    private bool Running = false;
    private bool patear = false;

    public bool ConArma = false;

    public bool ConGun = false;

    public LayerMask enemyLayer; 

    public int maxHealth = 3; // Salud máxima del personaje

    public int CurrentHealth;

    private Vector3 initialPosition; // Posición inicial del jugador




    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        CurrentHealth = maxHealth;

        initialPosition = transform.position;

        // Inicialmente desactivar todas las armas
        foreach (var arma in armas)
        {
            arma.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Running = true;
        }
        else if(Input.GetKeyUp(KeyCode.Z))
        {
            Running = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Running)
            {
                playerAnimator.SetTrigger("RunJump");
            }
            else
            {
                playerAnimator.SetTrigger("Jump");
            }
        }

        //Acciones

        if(Input.GetKeyDown(KeyCode.I))
        {

            patear = true;
            playerAnimator.SetTrigger("Patada");

        }
        else if(Input.GetKeyUp(KeyCode.I))
        {
            patear = false;
        }

        // Lógica para activar o desactivar las armas
        if (Input.GetKeyDown(KeyCode.O))
        {
            // Desactivar el arma actual
            if (armaSeleccionada >= 0 && armaSeleccionada < armas.Count)
            {
                armas[armaSeleccionada].SetActive(false);

                if(armas[armaSeleccionada].name == "Arma (1)")
                {
                    ConGun = false;
                }
            }

            // Seleccionar la siguiente arma
            armaSeleccionada = (armaSeleccionada + 1) % (armas.Count + 1);

            // Activar la nueva arma seleccionada
            if (armaSeleccionada >= 0 && armaSeleccionada < armas.Count)
            {
                armas[armaSeleccionada].SetActive(true);
                ConArma = true;

                if(armas[armaSeleccionada].name == "Arma (1)" )
                {
                    ConGun = true;
                }
                else
                {
                    ConGun = false;
                }
            }
            else
            {
                ConArma = false;
                ConGun = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ConArma && armaSeleccionada == 1)
            {
                // Ataque con el hacha (supongamos que el hacha es el primer arma en la lista)
                playerAnimator.SetTrigger("Ataque");
                PerformAttack();
                PerformAttack2();
            }
            else if (!ConArma)
            {
                // Golpe sin arma
                playerAnimator.SetTrigger("Golpe");
                PerformAttack();
                PerformAttack2();
            }
        }

        playerAnimator.SetFloat("Speed",Input.GetAxis("Vertical"));
        playerAnimator.SetFloat("Direction",Input.GetAxis("Horizontal"));
        playerAnimator.SetBool("Running",Running);
        playerAnimator.SetBool("patear", patear);
        playerAnimator.SetBool("ConArma", ConArma);
        playerAnimator.SetBool("ConGun",ConGun);


    }

    //Daño que recibe Zombie 1
    private void PerformAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, 1f, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            ZombieScript zombieScript = enemy.GetComponent<ZombieScript>();
            if (zombieScript != null)
            {
                zombieScript.ReceiveDamage();
            }
        }
    }

    //Daño que recibe  Zombie 2
    private void PerformAttack2()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, 1f, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            ZombieScript2 zombieScript2 = enemy.GetComponent<ZombieScript2>();
            if (zombieScript2 != null)
            {
                zombieScript2.ReceiveDamage();
            }
        }
    }

    public void ReceiveDamage()
    {
        // Activar la animación de daño
        playerAnimator.SetTrigger("Recibedaño");
        CurrentHealth--; // Reducir salud
        if (CurrentHealth <= 0)
        {
            Die(); // Llamar a la función de muerte si la salud llega a cero
        }
    }

    //Evento
    public void DealDamage()
    {
        PerformAttack();
    }

    //Evento2
    public void DealDamage2()
    {
        PerformAttack2();
    }

    private void Die()
    {
        playerAnimator.SetTrigger("Muerte"); // Activar la animación de muerte
  

        ResetPosition(); // Restablecer la posición del jugador
    }

    private void ResetPosition()
    {
        transform.position = initialPosition;
        CurrentHealth = maxHealth; // Reinicia la salud del jugador
        // Restablece cualquier otra variable de estado necesario
    }


}
