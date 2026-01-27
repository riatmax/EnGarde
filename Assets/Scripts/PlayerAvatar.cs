using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] private float moveSpeed;
    private PlayerInput inputActions;
    private Vector2 moveInput;


    public Animator ani;
    public float distChars;
    public bool closing = true;

    private void Awake()
    {
        inputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
