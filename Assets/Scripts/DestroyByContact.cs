using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    public GameObject explosion, playerExplosion;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundary")
        {
            return;
        }

        //this is to make explosion effects
        GameObject clone = Instantiate(explosion, transform.position, transform.rotation);

        if(other.tag == "Player") {
            GameObject pClone = Instantiate(playerExplosion, other.transform.position, other.transform.rotation);       
        }

        //order doesnt matter because by the end of the frame
        Destroy(other.gameObject);
        Destroy(gameObject); //the asteriod and all its children
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
