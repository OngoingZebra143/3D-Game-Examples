using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingwithRollABall : MonoBehaviour
{
    public float JumpForce = 10f;
    public float GravityModifier = 1f;
    public float OutOfBounds = -10f;
    public bool IsOnGround = true;
    public float Speed = 10f;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _isAtCheckpoint = false;
    private Vector3 _startingPosition;
    private Vector3 _checkpointPosition;
    private Rigidbody _playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _playerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }

        if(transform.position.y < OutOfBounds)
        {
            if(_isAtCheckpoint)
            {
                transform.position = _checkpointPosition;
            }
            else
            {
                transform.position = _startingPosition;
            }
        }
    }
    
    void FixedUpdate()
    {
         Vector3 movement = new Vector3(_horizontalInput, 0.0f, _verticalInput);
         
        _playerRigidbody.AddForce(movement);
    }   
    private void OnCollisionEnter(Collision collision)
    {    
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            _checkpointPosition = other.gameObject.transform.position;
        }
        if(other.gameObject.CompareTag("Endpoint"))
        {
            _isAtCheckpoint = false;
            transform.position = _startingPosition;
        }
    }
}
