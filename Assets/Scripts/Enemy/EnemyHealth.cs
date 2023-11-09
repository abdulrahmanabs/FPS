using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    public float currentHealth { get; private set; }
    void Awake()
    {
        currentHealth = maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        print("Enemy currnet health is : " + currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    
}
