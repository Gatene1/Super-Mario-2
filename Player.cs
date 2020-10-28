using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpSound;

    [SerializeField] private Animator _anim;
    [SerializeField] private Sprite _pickUpSprite;

    [SerializeField] private float _walkingSpeed = 6.5f;
    [SerializeField] private float _runModifier = 3.5f;
    [SerializeField] private float _jumpVelocity = 15f;
    [SerializeField] private float _gravity = 1.0f;

    private SpriteRenderer _sr;
    private AudioSource audio;
    private CharacterController _controller;
    private float _yVelocity;
    private bool _higherJump = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        audio = GetComponent<AudioSource>();

        //_anim.SetBool("Idle", true);
        _anim.SetBool("Idle", false);
        _anim.SetBool("Walking", false);
        _anim.SetBool("Running", false);
        _anim.SetBool("Jumping", true);

        _sr = gameObject.GetComponent<SpriteRenderer>();
        _controller = GetComponent<CharacterController>();

        _yVelocity = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        MovingAndJumping();
        PickUp();
    }
    void MovingAndJumping()
    {
        audio.clip = _jumpSound;

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = Input.GetButton("Submit") ? direction * (_walkingSpeed + _runModifier) : direction * _walkingSpeed;
        //Modified line directly above to be Ternary to include _runModifier in equation if holding Enter.

        if (horizontalInput > 0 && !Input.GetButton("Submit"))  //Check to see if player is moving Right and not holding Enter
        {
            _sr.flipX = false;
            _anim.SetBool("Walking", true);
            _anim.SetBool("Running", false);
            _anim.SetBool("Idle", false);
        }
        else if (horizontalInput > 0 && Input.GetButton("Submit")) //Check if moving Right and holding Enter
        {
            _sr.flipX = false;
            _anim.SetBool("Walking", false);
            //_anim.SetBool("Running", true);
            _anim.SetBool("Idle", false);
        }
        else if (horizontalInput < 0 && !Input.GetButton("Submit")) //Check if moving Left and not holding Enter
        {
            _sr.flipX = true;
            _anim.SetBool("Walking", true);
            _anim.SetBool("Running", false);
            _anim.SetBool("Idle", false);
        }
        else if (horizontalInput < 0 && Input.GetButton("Submit")) //Check if moving Left and holding Enter
        {
            _sr.flipX = true;
            _anim.SetBool("Walking", false);
            //_anim.SetBool("Running", true);
            _anim.SetBool("Idle", false);
        }
        else if (horizontalInput == 0)
        {
            _anim.SetBool("Walking", false);
            _anim.SetBool("Running", false);
            _anim.SetBool("Idle", true);
        }        

        if (_controller.isGrounded && Input.GetButtonDown("Jump")) //If jumping and on the ground, _yVelocity will be 19.0f each frame.
        {
            _yVelocity = _jumpVelocity;
            audio.Play();
            _anim.SetBool("Jumping", true);

            if (Input.GetButton("Submit"))
            {
                _anim.SetBool("Walking", false);
                _anim.SetBool("Running", true);
            }
            else
            {
                _anim.SetBool("Walking", true);
                _anim.SetBool("Running", false);
            }

        }
        else //If in the air, or if not pressing jump, _yVelocity will decrease the amount of _gravity each frame
        {
            _yVelocity -= _gravity;
        }

        if (_controller.isGrounded && !Input.GetButtonDown("Jump"))
        {
            _anim.SetBool("Jumping", false);
            _higherJump = false;

        }

        if (transform.position.y >= -.72 && Input.GetButton("Jump")) //If Jumping and holding the spacebar at a certain
        {                                                            //point, jump higher.
            if (_higherJump == false)
            {
                _yVelocity += 7.0f;
                _higherJump = true;
            }


        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void PickUp()
    {
        if (Input.GetButton("Submit") && _anim.GetBool("Idle"))
        {
            _anim.SetBool("Pick_Up", true);
        }
        else if (Input.GetButtonUp("Submit") && _anim.GetBool("Idle"))
            _anim.SetBool("Pick_Up", false);
    }
}
