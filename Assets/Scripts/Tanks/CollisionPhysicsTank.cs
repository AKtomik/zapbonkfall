using Unity.VisualScripting;
using UnityEngine;

public class CollisionPhysicsTank : MonoBehaviour
{
    private float collisionForce; //offensive power bump
    private float weight; //defensive power bump

    //temp
    public float bumpForce; //wall bounce force
    private void Start()
    {
        collisionForce = gameObject.GetComponent<Tank>().collisionForce; 
        weight = gameObject.GetComponent<Tank>().weight;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter2D(Collision2D otherCollision)
    {
        GameObject other = otherCollision.gameObject;
        Vector2 impactDirection = (other.transform.position - transform.position).normalized;
        if (other.tag == "Tank")
        {
            float otherTankWeight = other.GetComponent<Tank>().weight;


            //push the other tank
            other.GetComponent<Rigidbody2D>().AddForce(impactDirection * (1 + collisionForce - otherTankWeight), ForceMode2D.Impulse);
        }
        else if (other.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().AddForce(impactDirection * (1 / weight  *bumpForce ), ForceMode2D.Impulse);
        }
    }


    private void OnTriggerExit2D(Collider2D collision) //to detect when a tank fall
    {
        GameObject other = collision.gameObject;

        if (other.tag != "Ground") return;

        GameManager.instance.PlayerDied(gameObject);
    }
}
