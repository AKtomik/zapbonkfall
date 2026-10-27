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
        collisionForce = gameObject.GetComponent<StatsTank>().collisionForce; 
        weight = gameObject.GetComponent<StatsTank>().weight;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter2D(Collision2D otherCollision)
    {
        GameObject other = otherCollision.gameObject;
        Vector2 impactDirection = (other.transform.position - transform.position).normalized;
        if (other.tag == "Tank")
        {
            float otherTankWeight = other.GetComponent<StatsTank>().weight;


            //push the other tank
            other.GetComponent<Rigidbody2D>().AddForce(impactDirection * (1 + collisionForce - otherTankWeight), ForceMode2D.Impulse);
        }
        else if (other.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().AddForce(impactDirection * (1 / weight  *bumpForce ), ForceMode2D.Impulse);
        }
    }
}
