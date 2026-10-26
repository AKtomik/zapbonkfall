using Unity.VisualScripting;
using UnityEngine;

public class CollisionPhysicsTank : MonoBehaviour
{
    private float collisionForce;
    private float weight;
    private void Start()
    {
        //A DECOMMENTER UNE FOIS LE CONTROLLER TERMINE
        //collisionForce = gameObject.GetComponent<NomDuScriptStat>().collisionForce; 
        //weight = gameObject.GetComponent<NomDuScriptStat>().weight;


        //A SUPPRIMER UNE FOIS CONTROLLER TERMINE
        collisionForce = 1;
        weight = 1;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisonEnter2D(Collider other)
    {
        Vector2 impactDirection = (other.transform.position - transform.position).normalized;
        if (other.tag == "Tank")
        {
            //A COMPLETER ET DECOMMENTER
            //float otherTankWeight = other.GetComponent<Script des stats>().weight;
            /*temp*/ float otherTankWeight = 1;


            //repousse le tank
            other.GetComponent<Rigidbody2D>().AddForce(impactDirection * (1 + collisionForce - otherTankWeight), ForceMode2D.Impulse);
        }
        else if (other.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().AddForce(impactDirection * (1 / weight /* *bumpForce */), ForceMode2D.Impulse);
        }
    }
}
