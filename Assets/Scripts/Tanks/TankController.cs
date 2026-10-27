using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TankController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float enableGyroAngleMax = 90f;
    
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.EnableGyro(enableGyroAngleMax);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    	rb.AddForce(moveSpeed * Time.deltaTime * InputManager.ReadMovement2D());
    }
}
