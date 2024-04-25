using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public bool damagePossible;
    public PlayerInsomnia playerInsomnia; 
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        damagePossible = true;
    }


    // Update is called once per frame

    //amount being damage to be dealt; duration being the number of times this occurs
    public IEnumerator TakeDamage(int amount, int duration)
    {
        if(damagePossible == true)
            {
            for (int i = duration; i > 0; i--)
            {
                health -= (amount + (amount * playerInsomnia.insomnia));
                healthBar.fillAmount = health / 100;
                print("health is now " + health + "damage taken" + amount);
                yield return new WaitForSeconds(1f);
                if (health <= 0)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                }
        }

    }
    }
}
