using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour, IReset
{   
    private Controls controls;
    public Animator animator;

    public Transform barrel;
    public GameObject ammo;
    public float power;

    private GameObject active_ball;
    private AudioSource soundFX;

    private void Awake()
    {
        soundFX = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        controls = controls == null ? new Controls() : controls;
        controls.Player.Space.performed += Request_Cannon_Fire;
        controls.Player.Space.Enable();
    }

    private void Request_Cannon_Fire(InputAction.CallbackContext context)
    {
        animator.SetTrigger("Fire");
        Game_Manager.instance.Attempt_Completion();
        controls.Player.Space.Disable();
    }

    private void Fire_Cannon()
    {
        if (soundFX) { soundFX.pitch = Random.Range(1.10f, 1.30f); soundFX.Play(); }
        active_ball = Instantiate(ammo, barrel.position, Quaternion.identity);
        active_ball.GetComponent<Rigidbody2D>().AddForce(transform.up * (power * 100), ForceMode2D.Force);
    }

    public void Reset()
    {
        Destroy(active_ball);
        controls.Player.Space.Enable();
    }

    public void OnDisable()
    {
        controls.Player.Space.Disable();
    }
}
