using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

[System.Serializable]
public class CubeBehaviour : MonoBehaviour
{
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public bool isColliding;
    public bool debug;
    public List<CubeBehaviour> contacts;
    public float mass;
    public Vector3 velocity;
    public Vector3 acceleration;
    public bool gravity;
    public bool isGround;
    public bool onTheGround;

    private MeshFilter meshFilter;
    private Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        debug = false;
        meshFilter = GetComponent<MeshFilter>();

        bounds = meshFilter.mesh.bounds;
        size = bounds.size;

        acceleration =new Vector3(0.0f, -9.8f, 0.0f);
        velocity = new Vector3( 0.0f, 0.0f, 0.0f);
        //gravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
    }

    void FixedUpdate()
    {
        // physics related calculations
        if (gravity&&!onTheGround)
        {
            ApplyGravity();
        }
        if(onTheGround)
        {
            acceleration.y = 0.0f;
            velocity.y = 0.0f;
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireCube(transform.position, Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), transform.localScale));
        }
    }

    void ApplyGravity()
    {
        float deltaTime = 1.0f / 60.0f;
        velocity = velocity + acceleration * deltaTime;
        transform.position = transform.position + velocity * deltaTime;
    }
}
