using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfighter_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    public float movementSpeed = 10f;
    public float rotationSpeed = 10f;
    private Transform m_transform;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_transform = this.transform;
    }

    private void rotateDirection()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        m_transform.rotation = Quaternion.Slerp(m_transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        rotateDirection();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        //rb.velocity = movement;
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
