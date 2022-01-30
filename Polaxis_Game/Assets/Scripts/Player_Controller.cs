using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour, IControllable
{
    //Class References
    private Controls controls;
    private Level_Data level_data;

    //Moving Blocks
    private GameObject holding;
    public LayerMask Movable;


    private void OnEnable()
    {
        controls = controls == null ? new Controls() : controls;
        controls.Player.Spawn_Positive.performed += Request_Positive_Spawn;
        controls.Player.Spawn_Negative.performed += Request_Negative_Spawn;
        controls.Player.Left_Click.performed += Request_Pickup;
        controls.Player.Left_Click.canceled += Request_Placement;
        controls.Player.Spawn_Negative.Enable();
        controls.Player.Spawn_Positive.Enable();
        controls.Player.Left_Click.Enable();
        controls.Player.Pointer.Enable();
    }

    private void Start()
    {
        level_data = Game_Manager.instance.Level_Data;
    }

    private void Update()
    {
        Move_Item();
    }

    private void Request_Placement(InputAction.CallbackContext obj)
    {
        if (holding) {holding = null;}
    }

    private void Request_Pickup(InputAction.CallbackContext obj)
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(controls.Player.Pointer.ReadValue<Vector2>());
        Debug.Log(target);
        RaycastHit2D hit = Physics2D.Raycast(target, Vector3.forward * 1, 1f, Movable);
        Debug.DrawRay(target, Vector3.forward * 1, Color.green);
        if (hit.collider) { holding = hit.collider.gameObject; }
    }

    private void Move_Item()
    {
        if (!holding) { return; }
        Vector2 target = Camera.main.ScreenToWorldPoint(controls.Player.Pointer.ReadValue<Vector2>());
        holding.transform.position = Vector3.Lerp(holding.transform.position, target, 0.2f);
    }

    private void Request_Negative_Spawn(InputAction.CallbackContext context)
    {
        if (level_data.Magnet_Count < level_data.Magnet_Limit)
        {
            Vector2 pos = new Vector2(Random.Range(-3, 4), Random.Range(-3, 4));
            Instantiate(level_data.Negative_Prefab, pos, Quaternion.identity);
            level_data.Magnet_Count += 1;
        }
    }

    private void Request_Positive_Spawn(InputAction.CallbackContext context)
    {
        if (level_data.Magnet_Count < level_data.Magnet_Limit)
        {
            Vector2 pos = new Vector2(Random.Range(-3, 4), Random.Range(-3, 4));
            Instantiate(level_data.Positive_Prefab, pos, Quaternion.identity);
            level_data.Magnet_Count += 1;
        }
    }

    private void OnDisable()
    {
        controls.Player.Spawn_Negative.Disable();
        controls.Player.Spawn_Positive.Disable();
    }

    public void Disable_Controls()
    {
        controls.Player.Disable();
    }

    public void Enable_Controls()
    {
        controls.Player.Enable();
    }
}
