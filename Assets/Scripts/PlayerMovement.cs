using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class PlayerMovement : MonoBehaviour
{
	public PlayerStats playerStats;
	public Material[] material;
	Renderer rend;
	public float Speed;
	public float DodgeMultiplier;
	public Image staminaStatus;


	private Rigidbody2D _rb;
	private Vector3 _lastMovement;

	private bool _coolDown;

	private bool _movementPossible;
	
	public float StunDuration;

	public int bearTrapDamage;

	void Start()
	{
		_coolDown = false;
		_movementPossible = true;
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[0];
		staminaStatus.color = Color.green;
	}

    void Update()
	{
		if (_movementPossible == true)
		{
			//Basic movement, WASD
			if (Input.GetKey(KeyCode.W))
			{
				_lastMovement = Vector3.up * Time.deltaTime * Speed;
				transform.Translate(_lastMovement);
			}

			if (Input.GetKey(KeyCode.S))
			{
				_lastMovement = Vector3.down * Time.deltaTime * Speed;
				transform.Translate(_lastMovement);
			}

			if (Input.GetKey(KeyCode.D))
			{
				_lastMovement = Vector3.right * Time.deltaTime * Speed;
				transform.Translate(_lastMovement);
			}

			if (Input.GetKey(KeyCode.A))
			{
				_lastMovement = Vector3.left * Time.deltaTime * Speed;
				transform.Translate(_lastMovement);
			}

			//Dodge function, space 
			if (Input.GetKey(KeyCode.Space) && _coolDown == false)
			{
				transform.Translate(_lastMovement * DodgeMultiplier);
				staminaStatus.color = Color.black;
				StartCoroutine(DodgeInvincibility());
				StartCoroutine(Cooldown());
			}
		}
	}
	private IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(0.1f);
		_coolDown = true;
		yield return new WaitForSeconds(2);
		_coolDown = false;
		staminaStatus.color = Color.green;
	}

	private IEnumerator DodgeInvincibility()
    {
		playerStats.damagePossible = false;
		yield return new WaitForSeconds(1f);
		playerStats.damagePossible = true;
	}

    private IEnumerator Stun(Collider2D bearTrap)
    {
        _movementPossible = false;
        yield return new WaitForSeconds(StunDuration);
        _movementPossible = true;
        Destroy(bearTrap.transform.parent.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TrapTag")
        {
            print("Trapped!");
            StartCoroutine(Stun(other));
            playerStats.TakeDamage(bearTrapDamage, 1);
        }
    }
}
