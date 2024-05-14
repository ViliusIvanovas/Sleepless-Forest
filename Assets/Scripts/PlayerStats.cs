using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public bool damagePossible;
    public float damageMultiplier;
    public Image healthBar;
    public TMP_Text healthValuesText;
    public Animator animator;
    bool healthValuesOn;
    public float insomnia;
    public int nightEssence;
    public TextMeshProUGUI nightEssenceText;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        damagePossible = true;
        insomnia = 0;
        healthValuesOn = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            insomnia += 0.1f;
            print("Insomnia is" + insomnia);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (healthValuesOn == true) {
                animator.SetTrigger("turnOff");
                healthValuesOn = false;
            }
            else
            {
                animator.SetTrigger("turnOn");
                healthValuesOn = true;
            }
        }
    }

    // Update is called once per frame

    //amount being damage to be dealt; duration being the number of times this occurs
    public IEnumerator TakeDamage(int amount, int duration)
    {
        if (damagePossible == true)
        {
            for (int i = duration; i > 0; i--)
            {
                health -= (amount + (amount * insomnia));
                healthBar.fillAmount = health / maxHealth;
                healthValuesText.text = health.ToString() + "/" + maxHealth.ToString();
                yield return new WaitForSeconds(1f);

                if (health <= 0)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}
