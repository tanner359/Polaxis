using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Animator animator;

    public Vector2 detection_Size;
    public Vector2 detection_Offset;
    public bool showBounds;
    public LayerMask scorable;


    private void Update()
    {
        Target_Search();
    }

    public void Target_Search()
    {
        Collider2D target = Physics2D.OverlapBox((Vector2)transform.position + detection_Offset, detection_Size, 0, scorable);
        if (target)
        {
            //animator.SetTrigger("Score_Clip");
            Destroy(target);
            Game_Manager.instance.Complete_Level();
        }
    }

    private void OnDrawGizmos()
    {
        if (showBounds)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube((Vector2)transform.position + detection_Offset, detection_Size);
        }
    }


}
