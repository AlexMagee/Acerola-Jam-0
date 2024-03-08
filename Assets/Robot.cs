using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private Rigidbody2D _rigidbody;

    public Transform target;
    private bool travel = true;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody is null)
            Debug.LogError("Rigidbody2D is NULL!");
    }

    void FixedUpdate()
    {
        if(travel)
        {
            Vector2 direction = target.position - transform.position;
            direction.Normalize();
            _rigidbody.velocity = direction * speed;    
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.Equals(target))
        {
            travel = false;
            // Execute state interaction code
        }
    }
}
