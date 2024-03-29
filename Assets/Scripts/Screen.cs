using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> Enemies = new List<GameObject>();
    public bool IsUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTag")
        {
            
        }
        else if (collision.gameObject.tag == "PlayerTag")
        {
            IsUnlocked = true;

            foreach (var enemy in Enemies)
            {
                if (enemy != null)
                {
                    // Find script of enemy and disable being deactivated
                    enemy.gameObject.GetComponent<Enemy>().IsActive = true;
                }
            }
        }
    }
}
