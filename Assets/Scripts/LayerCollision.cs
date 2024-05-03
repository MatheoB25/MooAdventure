using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCollision : MonoBehaviour
{
    private bool _isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            _isColliding = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            _isColliding = false;
        }
    }
    public bool GetColliding()
    {
        return _isColliding;
    }


}


