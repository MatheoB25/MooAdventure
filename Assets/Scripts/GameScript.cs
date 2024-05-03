using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Android.Types;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameScript : MonoBehaviour
{
    [SerializeField] private Transform _mooTransform;
    private Vector2 _direction;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotateSpeed = 100f;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    private Vector3 _velocity;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private LayerCollision _scriptCollision;
    [SerializeField] private bool _jumpPressed = false;
    [SerializeField] private float _jumpImpulse = 2f; 

     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_scriptCollision.GetColliding() == true && _velocity.y < 0) 
        {
            _velocity.y = -0f;

        }

        else if (_scriptCollision.GetColliding() == false)
        {
            _velocity.y = _velocity.y - _gravity * Time.deltaTime;
        }
        Vector3 movement = _characterController.transform.forward * _direction.y * _moveSpeed * Time.deltaTime;
        movement.y = _velocity.y; //- _gravity * Time.deltaTime;
        _characterController.Move(movement);

        _characterController.transform.Rotate(Vector3.up * _direction.x * _rotateSpeed * Time.deltaTime);
        if (_direction.magnitude > 0.1 && _animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") == false)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
       
        

        
    }

    IEnumerator StopJumping()
    {
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("IsJumping", false);
    }
    
    
    public void Move(InputAction.CallbackContext Context)
    {

        _direction = Context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext Context)
    {
        if (Context.phase == InputActionPhase.Started && _scriptCollision.GetColliding() == true) 
        {
            _jumpPressed = true;
            _animator.SetBool("IsJumping", true);

            _velocity.y = _velocity.y + _jumpImpulse;
            StartCoroutine(StopJumping());
        }
        
        else if (Context.phase == InputActionPhase.Canceled)
        {
            _jumpPressed = false;
        }
        
  
    }
}





