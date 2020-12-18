using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletBehaviour : MonoBehaviour
{
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public float radius;
    public bool isColliding;
    public Vector3 position;

    public List<CubeBehaviour> contacts;

    public float speed;
    public Vector3 velocity;
    public Vector3 direction;
    public float range;

    public float mass;
    public bool stop;

    private MeshFilter meshFilter;
    private Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        bounds = meshFilter.mesh.bounds;
        size = bounds.size;
        radius = (max.x - min.x) / 2.0f;
        position = transform.position;
        velocity = direction * speed;
        mass = 5.0f;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        if(!stop)_Move();
        _CheckBounds();
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
    }

    private void _Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > range)
        {
            Destroy(gameObject);
        }
    }


}
