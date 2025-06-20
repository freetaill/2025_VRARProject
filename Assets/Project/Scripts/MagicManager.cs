using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagicManager : MonoBehaviour
{
    public GameObject controller = null;
    public Transform attachPoint;
    public GameObject hitEffect;
    public InputActionProperty shotMagicButton;

    Rigidbody rb;
    Vector3 offset;
    Ray ray;
    bool isFire = false;

    public float magicSpeed = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (controller != null && offset != Vector3.zero)
        {
            if (!isFire)
            {
                //get controller transform
                attachPoint.transform.position = offset + controller.transform.position;
                attachPoint.transform.rotation = controller.transform.rotation;
                //fallow controller
                transform.position = attachPoint.transform.position;
                ray = new Ray(transform.position, transform.forward);
                Debug.DrawRay(transform.position, transform.forward, Color.red);
                if (shotMagicButton.action.WasPressedThisFrame())
                {
                    //증폭 3회
                    //OnDrawRay(true);
                }
                if (shotMagicButton.action.WasCompletedThisFrame())
                {
                    //일정 방향으로 발사
                    Debug.Log("발사");
                    Fire();
                    PlayerMenuManager playermanumanager = FindAnyObjectByType<PlayerMenuManager>();
                    playermanumanager.ShootedMagic();
                }
            }
            else if (isFire)
            {
                rb.linearVelocity = attachPoint.transform.forward * magicSpeed;
            }
        }
    }
    void Fire()
    {
        if (rb != null)
        {
            isFire = true;
            rb.isKinematic = false; // 이제부터 물리 작동
            //rb.AddForce(transform.forward * magicSpeed);
        }

        // 필요하다면 일정 시간 후 자동 파괴
        Destroy(gameObject, 5f);
    }

    void OnDrawRay(bool isDraw)
    {
        ray = new Ray(transform.position, transform.forward);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Instantiate(hitEffect, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
        Debug.Log("충돌함");
    }

    public void Getcontroller(GameObject getController)
    {
        controller = getController;
        offset = new Vector3(0.035f, 0.04f, -0.07f);
    }
}
