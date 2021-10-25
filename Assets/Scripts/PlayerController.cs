using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb2;
    private CircleCollider2D _collider2D;
    public float MoveSpeed;
    public Camera Camera;
    private float _moveX;
    private float _moveY;
    

    void Start()
    {
        _rb2 = gameObject.GetComponent<Rigidbody2D>();
        _collider2D = gameObject.GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(_moveX, _moveY, 0).normalized;
        _rb2.velocity = moveDir*MoveSpeed;
    }
}
