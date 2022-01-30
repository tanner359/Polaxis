using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public Animator animator;
    public LayerMask destroyable;

    public float detection_range = 1;

    private void Update()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detection_range, destroyable);
        if(targets.Length > 0)
        {
            foreach(Collider2D c in targets)
            {
                Destroy(c.gameObject);
                animator.SetTrigger("Pulse");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detection_range);
    }
}
