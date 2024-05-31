using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarArmas : MonoBehaviour
{
    public Arma arm;
    public int numeroArma;
    // Start is called before the first frame update
    void Start()
    {
        arm = GameObject.FindGameObjectWithTag("Player").GetComponent<Arma>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //arm.RecogerArma(numeroArma);
            Destroy(gameObject);
        }
    }
}

