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

    [SerializeField] List<GameObject> CommonDrops = new List<GameObject>();
    [SerializeField] List<GameObject> RareDrops = new List<GameObject>();

    public bool hasHealthBar = true;
    private float healthBarLifeTime = 2.0f;
    private Projectile.projectileEffects status = Projectile.projectileEffects.None;
    public float effectTime = 5.0f;
    private float currentEffectTime;
    private float timeBetweenDamage = 0.5f;
    private float remainingTimeBetweenDamage = 0.5f;

    void Start()
    {
        currentHealth = maxHealth;
        currentEffectTime = 0.0f;

        if (hasHealthBar)
        {
            healthBarBackground.enabled = false;
            healthBarFill.enabled = false;
        }
        
    }

    public void Update()
    {
        if (status == Projectile.projectileEffects.Fire)
        {
            currentEffectTime -= Time.deltaTime;
            remainingTimeBetweenDamage -= Time.deltaTime;

            if (remainingTimeBetweenDamage <= 0)
            {
                TakeDamage(2);
                remainingTimeBetweenDamage = timeBetweenDamage;
            }

            if (currentEffectTime <= 0.0f)
            {
                status = Projectile.projectileEffects.None;
            }
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

        if (Application.isPlaying)
        {
            int random = Random.Range(0, 100);
            int randObject;

            if (random <= 5)
            {
                if (RareDrops.Count > 0)
                {
                    Debug.Log("Dropping Rare");
                    randObject = Random.Range(0, RareDrops.Count);
                    GameObject dropped = Instantiate(RareDrops[randObject], transform.position, transform.rotation);
                }
            }
            else if (random <= 25)
            {
                if (CommonDrops.Count > 0)
                {
                    Debug.Log("Dropping Common");
                    randObject = Random.Range(0, CommonDrops.Count);
                    GameObject dropped = Instantiate(CommonDrops[randObject], transform.position, transform.rotation);
                }
            }

            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<TopDownCharacterController>().Die();
            }
            else if (gameObject.GetComponent<Minotaur>() != null)
            {
                gameObject.GetComponent<Minotaur>().Die();
                Destroy(this);
            }
        }

        Destroy(gameObject);
        Destroy(this);
    }

    public void ShowHealthBar()
    {
        healthBarFill.fillAmount = (float)currentHealth / (float)maxHealth;
        healthBarBackground.enabled = true;
        healthBarFill.enabled = true;

        StopAllCoroutines();
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

    public void ApplyEffect(Projectile.projectileEffects effect)
    {
        status = effect;
        currentEffectTime = effectTime;
    }
}
