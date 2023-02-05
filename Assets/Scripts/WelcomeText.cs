using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyMe), 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
    
}
