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
    private float reduction = 0.05f;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Log(myRigidbody2D.velocity);
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
            Debug.Log(col.contacts.First());
            Debug.Log(velocity);
            Vector3 worldPoint = col.contacts.First().point + velocity.normalized*reduction;
            worldPoint.z = 0;
            Vector3Int tile = tilemap.WorldToCell(worldPoint);
            Debug.Log(tile);
            tilemap.SetTile(tile, null);
            Instantiate(tileDestroy, worldPoint, Quaternion.identity);
        }
        Instantiate(bulletDestroy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
