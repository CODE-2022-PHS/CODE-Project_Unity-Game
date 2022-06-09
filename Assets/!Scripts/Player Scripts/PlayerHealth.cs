using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public GameObject player;
    
    //public static PlayerHealth use;
    public Image healthBar;
    public float currentHealth;
    float maxHealth = 100f;
    float lerpSpeed;
    //float healingPoints = .01f;
    //float damagePoints;

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //maxHealth = GlobalData.maxHealth;
        currentHealth = GlobalData.currHealth;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        lerpSpeed = 1.5f * Time.deltaTime;

        HealthBar();
        HealthColor();
        //Damage(damagePoints);
    }

    public void Damage(float damagePoints)
    {
        if(currentHealth > 0)
        {
            GlobalData.currHealth -= damagePoints;
        }
        /*else
        {
            currentHealth = 0;
        }*/

        if(currentHealth <= 0)
        {
            GlobalData.hasDied = true;
            SceneManager.LoadScene("GameOver");
        }
        
        //healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, lerpSpeed);

    }

    /*public void Heal(float healingPoints)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += healingPoints;
        }
    }*/

    public void HealthBar()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, lerpSpeed);
    }

    public void HealthColor()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, currentHealth/maxHealth);
        healthBar.color = healthColor;
    }
}