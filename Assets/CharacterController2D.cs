using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private PlayerMovementActions playerActions;
    private Rigidbody2D _rigidbody;
    private Vector2 moveInput;

    private GameObject grabTarget;
    private GameObject grabbedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        playerActions = new PlayerMovementActions();

        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody is null)
            Debug.LogError("Rigidbody2D is NULL!");
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
    }

    private void onDisable()
    {
        playerActions.Player.Disable();
    }

    void Update()
    {
        if(grabbedItem == null && grabTarget != null && playerActions.Player.Interact.ReadValue<float>() == 1)
        {
            grabbedItem = grabTarget;
            grabbedItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            grabbedItem.GetComponent<Collider2D>().enabled = false;
            grabbedItem.transform.parent = transform.GetChild(0);
            grabTarget = null;
        } else if(grabbedItem != null && playerActions.Player.Interact.ReadValue<float>() == 1)
        {
            
            grabbedItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            grabbedItem.GetComponent<Collider2D>().enabled = true;
            grabbedItem.transform.parent = null;
            grabbedItem = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = playerActions.Player.Move.ReadValue<Vector2>();
        _rigidbody.velocity = moveInput * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        grabTarget = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(grabTarget == col.gameObject)
        {
            grabTarget = null;
        }
    }
}
