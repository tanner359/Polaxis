using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movable : MonoBehaviour
{
    [SerializeField] public List<Link> links = new List<Link>();
    [SerializeField] public enum Options {Enabled, Disabled}
    [SerializeField] public Options loopingTrack = Options.Disabled;

    [Range(0.01f,5f)] public float stopThreshhold = 0.01f;

    public void Add_New_Link()
    {
        Link newLink = null;
        if (links.Count == 0)
        {
            newLink = new Link(transform.position, transform.position + (transform.right * 2), 5f);
            links.Add(newLink);
            return;
        }

        Link prevLink = links[links.Count - 1];
        newLink = new Link(prevLink.location_02, prevLink.location_02 + (Vector2.right * 2), 5f);
        prevLink.location_02 = newLink.location_01;
        links.Add(newLink);       
    }

    private void Update()
    {
        Move();
    }

    int target = 0;
    Vector2 targetLocation = Vector2.zero;
    public void Move()
    {
        if(targetLocation == Vector2.zero){targetLocation = links[0].location_01;}

        if (Vector2.Distance(transform.position, targetLocation) > stopThreshhold)
        {
            transform.position += ((Vector3)targetLocation - transform.position).normalized * Time.deltaTime * links[target].speed;
            return;
        }

        if(targetLocation == links[0].location_01)
        {
            targetLocation = links[0].location_02;
        }
        else if (target != links.Count - 1)
        {
            target += 1;
            targetLocation = links[target].location_02;
        }
        else if (loopingTrack == Options.Enabled)
        {
            target = 0;
            targetLocation = links[0].location_01;
        }
    }
}

[System.Serializable]
public class Link
{
    [SerializeField] public Vector2 location_01;
    [SerializeField] public Vector2 location_02;

    public float speed;

    public Link prev;
    public Link next;

    public Link(Vector2 start, Vector2 end, float speed)
    {
        this.location_01 = start;
        this.location_02 = end;
        this.speed = speed;
    }

    public Vector2 Middle { get { return (location_01 + location_02) / 2; } }
}
