using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDPlatformer : MonoBehaviour
{
    public float JumpForce = 10f;
     public float turnSpeed = 20f;
    public float GravityModifier = 1f;
    public bool IsOnGround = true;
    public float Speed = 10f;
    private float _horizontalInput;
    private float _forwardInput;
    Vector3 m_Movement;
    Rigidbody _playerRigidbody;
    Quaternion m_Rotation = Quaternion.identity;


    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
    }

    // Update is called once per frame
    void Update()
    {

        _horizontalInput = Input.GetAxis("Horizontal");
        _forwardInput = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _playerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }
    }
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    
        _playerRigidbody.MovePosition (_playerRigidbody.position + m_Movement * Speed * Time.deltaTime);
        _playerRigidbody.MoveRotation (m_Rotation);
    }   
    private void OnCollisionEnter(Collision collision)
    {    
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }
}
