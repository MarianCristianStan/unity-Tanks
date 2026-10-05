using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{

     private float healthAmout = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Player"))
        {
            TankHealth playerHealth = other.GetComponent<TankHealth>();

            if (playerHealth.get_m_CurrentHealth() < 100)
            {
                playerHealth.addHealth(healthAmout);
                Destroy(gameObject);
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
