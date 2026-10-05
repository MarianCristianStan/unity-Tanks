using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasExplotion : MonoBehaviour
{
    public LayerMask m_TankMask;                        
    public ParticleSystem m_ExplosionParticles;         
    public AudioSource m_ExplosionAudio;                
    public float m_MaxDamage = 100f;                   
    public float m_ExplosionForce = 100f;             
    public float m_ExplosionRadius = 5f;
    public bool exploded = false;
    private float timer = 10f;
    public bool getExploded()
    {
        return exploded;
    }
    private void Start()
    {
  
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        // Go through all the colliders...
        if (other.CompareTag("Shell"))
        {
           
            for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // Add an explosion force.
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;

            float damage = CalculateDamage(targetRigidbody.position);

            targetHealth.TakeDamage(damage);
        }
            exploded = true;
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();    
            Invoke("destroyMyObj", 3);
            
        }

        //m_ExplosionParticles.Play();

    }
    private void Update()
    {
        
        
        
    }



    private void destroyMyObj()
    {
        Destroy(gameObject);

    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        float damage = relativeDistance * m_MaxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
