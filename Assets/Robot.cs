using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _holdRenderer;

    private bool travel = true;

    public enum stateTypes
    {
        Find,
        Grab,
        Drop,
        Interact,
        Travel
    }

    [Serializable]
    public struct RobotState
    {
        public Transform target;
        public stateTypes type;
    }

    [SerializeField]
    public RobotState[] states;
    private int state = 0;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody is null)
            Debug.LogError("Rigidbody2D is NULL!");

        _holdRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (_holdRenderer is null)
            Debug.LogError("SpriteRenderer is NULL!");
    }

    void FixedUpdate()
    {
        Vector2 direction = states[state].target.position - transform.position;
        Vector2 normalizedDirection = direction;
        normalizedDirection.Normalize();
        switch (states[state].type)
        {
            case stateTypes.Find:
                _rigidbody.velocity = normalizedDirection * speed;    
                break;
            case stateTypes.Travel:
                if(direction.magnitude < 0.25f)
                    TransitionState();
                else
                    _rigidbody.velocity = normalizedDirection * speed;
                break;
            case stateTypes.Grab:
                _holdRenderer.enabled = true;
                TransitionState();
                break;
            case stateTypes.Drop:
                _holdRenderer.enabled = false;
                TransitionState();
                break;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.Equals(states[state].target))
        {
            switch (states[state].type)
            {
                case stateTypes.Find:
                    TransitionState();
                    break;
            }
            
        }
    }

    void TransitionState()
    {
        state++;
        if(state > states.Length - 1)
        {
            Debug.Log(gameObject.name + " completed a cycle");
            state = 0;
        }
    }
}
