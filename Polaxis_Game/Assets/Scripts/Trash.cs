using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Level_Data level_data;
    private AudioSource audioSource;
    public Animator animator;
    public LayerMask destroyable;

    public float detection_range = 1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        level_data = Game_Manager.instance.Level_Data;
    }

    private void Update()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detection_range, destroyable);
        if(targets.Length > 0)
        {
            foreach(Collider2D c in targets)
            {
                Destroy(c.gameObject);
                audioSource.pitch = Random.Range(0.8f, 1.0f);
                audioSource.Play();
                animator.SetTrigger("Pulse");
                level_data.Magnet_Count -= 1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detection_range);
    }
}
