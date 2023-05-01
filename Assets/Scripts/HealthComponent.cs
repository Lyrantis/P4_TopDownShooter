using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    // Start is called before the first frame update

    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] Image healthBarFill;
    [SerializeField] Image healthBarBackground;
    public bool hasHealthBar = true;
    private float healthBarLifeTime = 2.0f;

    void Start()
    {
        currentHealth = maxHealth;

        if (hasHealthBar)
        {
            healthBarBackground.enabled = false;
            healthBarFill.enabled = false;
        }
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (hasHealthBar)
        {
            ShowHealthBar();
        }
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (hasHealthBar)
        {
            ShowHealthBar();
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Destroy(this);
    }

    public void ShowHealthBar()
    {
        healthBarFill.fillAmount = (float)currentHealth / (float)maxHealth;
        healthBarBackground.enabled = true;
        healthBarFill.enabled = true;

        StartCoroutine(HideHealthBar());
    }

    IEnumerator HideHealthBar()
    {
        yield return new WaitForSeconds(healthBarLifeTime);

        healthBarBackground.enabled = false;
        healthBarFill.enabled = false;
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
