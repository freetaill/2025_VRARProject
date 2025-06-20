using UnityEngine;

public class cubeinteracion : MonoBehaviour
{
    Rigidbody rb;
    float force = 30.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            GameObject otherObject = collision.gameObject;

            Vector3 normal = collision.contacts[0].normal;

            Vector3 KnokbackDirection = -normal;

            rb.AddForce(KnokbackDirection.normalized * force, ForceMode.Impulse);
        }
    }
}
