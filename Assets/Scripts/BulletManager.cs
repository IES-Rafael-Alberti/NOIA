using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject tileDestroy;
    [SerializeField] private GameObject bulletDestroy;

    public GameObject tiles;
    private Rigidbody2D myRigidbody2D;
    private float reduction = 0.1f;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Invoke(nameof(DestroyThis), 3.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        velocity = myRigidbody2D.velocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ramas"))
        {
            Tilemap tilemap = tiles.GetComponent<Tilemap>();
            /*
            Vector3 worldPoint = col.contacts.First().point + velocity.normalized*reduction;
            worldPoint.z = 0;
            */
            Vector3 worldPoint = col.contacts.First().point;
            worldPoint.z = 0;
            Vector3 normal = worldPoint - transform.position;
            worldPoint += normal.normalized*reduction;
            
            Vector3Int tile = tilemap.WorldToCell(worldPoint);
            tilemap.SetTile(tile, null);
            Instantiate(tileDestroy, worldPoint, Quaternion.identity);
        } else if (col.gameObject.CompareTag("robot"))
        {
            Destroy(col.gameObject);
        }
        
        Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
