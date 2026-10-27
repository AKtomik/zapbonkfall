using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    	rb.AddForce(moveSpeed * Time.deltaTime * InputManager.ReadMovement2D());
    }
}
