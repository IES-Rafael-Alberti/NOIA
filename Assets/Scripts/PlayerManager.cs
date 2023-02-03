using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    private Rigidbody2D myRigidbody2D;
    private Camera myCamera;
    
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private int maxJumpEnergy = 100;
    [SerializeField] private int jumpEnergy;
    [SerializeField] private float jumpForce = 0.1f;
    [SerializeField] private int jumpEnergyConsumption = 10;
    [SerializeField] private int jumpEnergyRecovery = 1;
    [SerializeField] private float speed = 0.3f;

    [SerializeField] private float bulletSpeed = 8.0f;
    [SerializeField] private float bulletOrigin = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCamera = Camera.main;
        jumpEnergy = maxJumpEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetAxis("Vertical") > 0) && jumpEnergy > 0)
        {
            myRigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            jumpEnergy -= jumpEnergyConsumption;
        }
        else if(jumpEnergy < maxJumpEnergy)
        {
            jumpEnergy += jumpEnergyRecovery;
        }

        myRigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, myRigidbody2D.velocity.y);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 destiny = (myCamera.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
            destiny.z = 0;
            Vector3 origin = destiny * bulletOrigin;
            GameObject newBullet = Instantiate(bulletPrefab, gameObject.transform.position + origin, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(destiny*bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
