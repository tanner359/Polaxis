using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float polar_force = 1f;
    public float range = 1f;
    public bool show_range;
    public Transform magnet_range;
    public LayerMask targetable;
    public enum Polarity {Positive, Negative};
    public Polarity polarity;

    private Controls controls;

    private AudioSource audioSource;
    public AudioClip spawnSound;

    private void Awake()
    {
        controls = controls == null ? new Controls() : controls;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(spawnSound);
    }

    private void FixedUpdate()
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

    private int numTargets = 0;
    private void Target_Search()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, range, targetable);
        if(targets.Length > 0)
        {
            foreach (Collider2D c in targets)
            {
                Calculate_Force(c.attachedRigidbody);
            }
            if(numTargets < targets.Length){
                Play_Audio();             
            }
            numTargets = targets.Length;
        }
        else
        {
            numTargets = 0;
        }
    }

    private void Play_Audio()
    {
        float audioTime = audioSource.time/audioSource.clip.length;
        if (!audioSource.isPlaying) 
        {
            audioSource.pitch = Random.Range(1.2f, 1.5f);
            audioSource.Play(); 
        }
    }

    private void OnDrawGizmos()
    {
        if (show_range && magnet_range)
        {
            magnet_range.localScale = Vector3.one * (range/3.35f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }


}
