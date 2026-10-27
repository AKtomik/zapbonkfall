using Unity.VisualScripting;
using UnityEngine;

public class ProtoAutoChase : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;
    [SerializeField] Transform player;
    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = ((Vector2)player.position - rb.position).normalized;
        rb.AddForce(moveSpeed * Time.deltaTime * direction);
    }
}
