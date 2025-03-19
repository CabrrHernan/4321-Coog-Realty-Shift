using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Break : MonoBehaviour
{
    public GameObject objectToReplace;
    public GameObject brokenObject;
    public Material MaterialRef;
    public float bForce = 1f;
    protected Rigidbody rb;
    private int active = 0;
    private GameObject newObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.linearVelocity.magnitude > bForce && active == 0)
        {
            active++;
            Destroy(objectToReplace);
            newObject = Instantiate(brokenObject, transform.position, transform.rotation);
            newObject.name = "brokenGlass";
            brokenObject.GetComponent<Renderer>().material = MaterialRef;
            rb.AddExplosionForce(10f, Vector3.zero, 0f);
        }
    }
}
