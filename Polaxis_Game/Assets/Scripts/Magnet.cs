using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float polar_force = 1f;
    public float range = 1f;
    public bool show_range;
    public LayerMask targetable;
    public enum Polarity {Positive, Negative};
    public Polarity polarity;

    private Controls controls;


    private void Awake()
    {
        controls = controls == null ? new Controls() : controls;
    }

    private void Update()
    {
        Target_Search();
    }

    private void Push(Rigidbody2D rb)
    {
        Vector2 dir = rb.transform.position - transform.position;
        rb.AddForce(dir.normalized * (polar_force * 100), ForceMode2D.Force);
    }

    private void Pull(Rigidbody2D rb)
    {
        Vector2 dir = rb.transform.position - transform.position;
        rb.AddForce(-dir.normalized * (polar_force * 100), ForceMode2D.Force);
    }

    private void Calculate_Force(Rigidbody2D rb)
    {
        switch (polarity)
        {
            case Polarity.Positive:
                Push(rb);
                break;
            case Polarity.Negative:
                Pull(rb);
                break;
        }
    }

    private void Target_Search()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, range, targetable);
        if(targets.Length > 0)
        {
            foreach (Collider2D c in targets)
            {
                Calculate_Force(c.attachedRigidbody);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (show_range)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }


}
