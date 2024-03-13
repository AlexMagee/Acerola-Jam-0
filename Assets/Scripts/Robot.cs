using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public bool isGameRobot = false;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _holdRenderer;
    private Animator _animator;
    private GameManager manager;
    private GameObject distraction;

    private bool travel = true;
    private float timer = -1;
    private bool interrupt = false;

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

        _animator = transform.GetComponent<Animator>();
        if (_animator is null)
            Debug.LogError("Animator is NULL!");

        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        Vector2 direction = states[state].target.position - transform.position;
        Vector2 normalizedDirection = direction;
        normalizedDirection.Normalize();
        if(distraction is null)
        {
            Debug.Log("Distraction is null");
        }
        else
        {
            if(isGameRobot)
            {
                direction = distraction.transform.position - transform.position;
                normalizedDirection = direction;
                normalizedDirection.Normalize();
                if(direction.magnitude > 0.25f)
                {
                    _rigidbody.velocity = normalizedDirection * speed;
                    _animator.SetFloat("velocity_y", _rigidbody.velocity.y);
                }  
                return; 
            }
        }
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
                if(interrupt && timer > 0.25)
                {
                    RevertState();
                    break;
                }
                if(timer == -1)
                {
                    timer = 1;
                } else if(timer < 0.25 && _holdRenderer.enabled == false)
                {
                    _holdRenderer.enabled = true;
                }
                timer -= Time.fixedDeltaTime;
                if(timer <= 0)
                {
                    TransitionState();
                    timer = -1;
                }
                break;
            case stateTypes.Drop:
                if(interrupt && timer > 0.25)
                {
                    RevertState();
                    break;
                }
                if(timer == -1)
                {
                    timer = 1;
                } else if(timer < 0.25 && _holdRenderer.enabled == true)
                {
                    _holdRenderer.enabled = false;
                }
                timer -= Time.fixedDeltaTime;
                if(timer <= 0)
                {
                    TransitionState();
                    timer = -1;
                }
                break;
        }
        _animator.SetFloat("velocity_y", _rigidbody.velocity.y);

        distraction = GameObject.FindWithTag("Distraction");
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

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.transform.Equals(states[state].target))
        {
            interrupt = true;
        }
    }

    void TransitionState()
    {
        interrupt = false;
        state++;
        if(state > states.Length - 1)
        {
            if(isGameRobot)
            {
                Debug.Log(gameObject.name + " completed a cycle");
                manager.IncrementCycle();
            }
            state = 0;
        }
    }

    void RevertState()
    {
        interrupt = false;
        timer = -1;
        state--;
        if(state < 0)
        {
            state = states.Length - 1;
        }
    }
}
