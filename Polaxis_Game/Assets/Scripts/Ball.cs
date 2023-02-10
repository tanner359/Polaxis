using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private void Awake()
    {
        m_AudioSource= GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = collision.relativeVelocity.magnitude;
        if (speed > 1.5f)
        {
            Debug.Log("PLAY: " + speed);
            m_AudioSource.pitch = 5/speed+0.001f;
            m_AudioSource.Play();
        }
    }
}
