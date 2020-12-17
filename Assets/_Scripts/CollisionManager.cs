using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public CubeBehaviour[] actors;
    public BulletBehaviour[] bullets;

    // Start is called before the first frame update
    void Start()
    {
        actors = FindObjectsOfType<CubeBehaviour>();
        bullets = FindObjectsOfType<BulletBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < actors.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {
                if (i != j)
                {
                    CheckAABBs(actors[i], actors[j]);
                }
            }
        }
        for (int i = 0; i < actors.Length; i++)
        {
            for (int j = 0; j < bullets.Length; j++)
            {
                CheckSphereAABB(bullets[j], actors[i]);
            }
        }
    }
    public static void CheckAABBs(CubeBehaviour a, CubeBehaviour b)
    {
        if ((a.min.x <= b.max.x && a.max.x >= b.min.x) &&
            (a.min.y <= b.max.y && a.max.y >= b.min.y) &&
            (a.min.z <= b.max.z && a.max.z >= b.min.z))
        {
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
                a.isColliding = true;
                if (b.isGround)
                {
                    a.onTheGround = true;
                }
                else if (b.onTheGround)
                {
                    a.onTheGround = true;
                }
            }
        }
        else
        {
            if (a.contacts.Contains(b))
            {
                a.contacts.Remove(b);
                a.isColliding = false;
                if (b.isGround)
                {
                    a.onTheGround = false;
                }
                else if (b.onTheGround)
                {
                    a.onTheGround = false;
                }
            }

        }
    }
    public static void CheckSphereAABB(BulletBehaviour a, CubeBehaviour b)
    {
        float ax = a.transform.position.x;
        float ay = a.transform.position.y;
        float az = a.transform.position.z;
        float x = Mathf.Max(b.min.x, Mathf.Min(ax, b.max.x));
        float y = Mathf.Max(b.min.y, Mathf.Min(ay, b.max.y));
        float z = Mathf.Max(b.min.z, Mathf.Min(az, b.max.z));
        float distance = Mathf.Sqrt((x - ax) * (x - ax) +
                                  (y - ay) * (y - ay) +
                                  (z - az) * (z - az));
        if (distance < a.radius+0.5f)
        {
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
                a.isColliding = true;
            }
        }
        else
        {
            if (a.contacts.Contains(b))
            {
                a.contacts.Remove(b);
                a.isColliding = false;
            }

        }
        
    }
}
