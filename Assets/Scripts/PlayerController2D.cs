using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private StateMachine _fsm;

    private Vector2 _moveInput;
    private Vector2 _lastMoveDirection;

    private const string IDLE_DOWN = "IdleDown";
    private const string IDLE_RIGHT = "IdleRight";
    private const string IDLE_UP = "IdleUp";
    private const string WALK_DOWN = "WalkDown";
    private const string WALK_RIGHT = "WalkRight";
    private const string WALK_UP = "WalkUp";

    private string _currentStateName;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        _lastMoveDirection = Vector2.down;

        _fsm = new StateMachine();

        _fsm.AddState("Idle", 
            onEnter: (state) =>
            {
                _rb.velocity = Vector2.zero;

                PlayAnimation(GetAnimStateName(_lastMoveDirection, false));
            },
            onLogic: (state) =>
            {
                _moveInput = GetInput();
            }
        );

        _fsm.AddState("Move", 
            onEnter: (state) =>
            {
            },
            onLogic: (state) =>
            {
                _moveInput = GetInput();
                _rb.velocity = _moveInput * _moveSpeed;

                if(_moveInput != Vector2.zero)
                {
                    _lastMoveDirection = _moveInput;
                    HandleSpriteFlip(_moveInput.x);

                    PlayAnimation(GetAnimStateName(_lastMoveDirection, true));   
                }
            }
        );

        _fsm.AddTransition("Idle", "Move", 
            (transition) =>_moveInput.magnitude > 0.1f
        );

        _fsm.AddTransition("Move", "Idle",
            (transition) => _moveInput.magnitude < 0.1f
        );

        _fsm.Init();
    }

    void Update()
    {
        _fsm.OnLogic();
    }

    Vector2 GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y).normalized;
    }
    void HandleSpriteFlip(float xInput)
    {
        if(xInput > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        else if(xInput < 0.01f) transform.localScale = new Vector3(-1, 1, 1);
    }
    string GetAnimStateName(Vector2 direction, bool isMoving)
    {
        bool useY = Mathf.Abs(direction.y) > Mathf.Abs(direction.x);

        if(useY)
        {
            if(direction.y > 0) return isMoving ? WALK_UP : IDLE_UP;
            else return isMoving ? WALK_DOWN : IDLE_DOWN;
        } 
        else
        {
            return isMoving ? WALK_RIGHT : IDLE_RIGHT;    
        }
    }
    void PlayAnimation(string newStateName)
    {
        if(_currentStateName == newStateName) return;

        _animator.Play(newStateName);
        _currentStateName = newStateName;
    }
}
