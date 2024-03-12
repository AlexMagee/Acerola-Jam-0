using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private Rigidbody2D _rigidbody;
    private Vector2 moveInput;

    private List<GameObject> grabTargets = new List<GameObject>();
    private GameObject grabbedItem;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody is null)
            Debug.LogError("Rigidbody2D is NULL!");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(grabbedItem == null && grabTargets.Count > 0 && context.performed)
        {
            grabbedItem = grabTargets[0];
            grabbedItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            grabbedItem.layer = LayerMask.NameToLayer("Carrying");
            grabbedItem.transform.parent = transform.GetChild(0);
            grabTargets.Remove(grabbedItem);
        }
        else if(grabbedItem != null && context.performed)
        {
            grabbedItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            grabbedItem.layer = LayerMask.NameToLayer("Default");
            grabbedItem.transform.parent = null;
            grabbedItem = null;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.velocity = moveInput * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Robot>() == null)
        {
            grabTargets.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        grabTargets.Remove(col.gameObject);
    }
}
