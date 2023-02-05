using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    
    private Rigidbody2D myRigidbody2D;
    private Camera myCamera;

    [SerializeField] private AudioSource bulletAudioSource;
    [SerializeField] private AudioSource jumpAudioSource;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject jumpEnergyBar;

    [SerializeField] private float maxJumpEnergy = 100.0f;
    [SerializeField] private float jumpEnergy;
    [SerializeField] private float jumpForce = 4.0f;
    [SerializeField] private float jumpEnergyConsumption = 4.5f;
    [SerializeField] private float jumpEnergyRecovery = 2.5f;
    [SerializeField] private float speed = 4.0f;

    [SerializeField] private float bulletSpeed = 2.0f;
    [SerializeField] private float bulletOrigin = 0.5f;
    [SerializeField] private GameObject ramas;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCamera = Camera.main;
        jumpEnergy = maxJumpEnergy;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetAxis("Vertical") > 0)
        {
            if (jumpEnergy > 0)
            {
                if (!jumpAudioSource.isPlaying)
                {
                    jumpAudioSource.Play();
                }
                else
                    myRigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
                jumpEnergy -= jumpEnergyConsumption;
            }
        }
        else if(jumpEnergy < maxJumpEnergy)
        {
            jumpEnergy += jumpEnergyRecovery;
        }

        jumpEnergy = jumpEnergy < 0 ? 0 : jumpEnergy;

        myRigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, myRigidbody2D.velocity.y);

        if (Input.GetMouseButtonDown(0))
        {
            bulletAudioSource.Play();
            Vector3 destiny = (myCamera.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
            destiny.z = 0;
            Vector3 origin = destiny * bulletOrigin;
            GameObject newBullet = Instantiate(bulletPrefab, gameObject.transform.position + origin, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(destiny*bulletSpeed, ForceMode2D.Impulse);
            newBullet.GetComponent<BulletManager>().tiles = ramas;
        }

        Vector3 jumpScale = jumpEnergyBar.transform.localScale;
        jumpScale.x = jumpEnergy / maxJumpEnergy;
        jumpEnergyBar.transform.localScale = jumpScale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(3);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(3));
    }
}
