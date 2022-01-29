using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    private Controls controls;

    public Transform barrel;
    public GameObject ammo;
    public float power;

    private void Awake()
    {
        controls = controls == null ? new Controls() : controls;
        controls.Player.Space.performed += Request_Cannon_Fire;
        controls.Player.Space.Enable();
    }

    private void Request_Cannon_Fire(InputAction.CallbackContext context)
    {
        Fire_Cannon();
        controls.Player.Space.Disable();
    }

    private void Fire_Cannon()
    {
        GameObject obj = Instantiate(ammo, barrel.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(transform.up * (power * 100), ForceMode2D.Force);
    }
}
