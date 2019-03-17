using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    public GameObject explosion, playerExplosion;
    public int scoreValue;
    private GameController gameController;

        // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null) 
        { 
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        { 
            Debug.Log("Cannot find GameController Script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundary")
        {
            return;
        }

    
        // make explosion effects
        GameObject clone = Instantiate(explosion, transform.position, transform.rotation);

      
        if(other.tag == "Player") {
            GameObject pClone = Instantiate(playerExplosion, other.transform.position, other.transform.rotation);       
        }

        gameController.AddScore(scoreValue);
        //order doesnt matter because by the end of the frame
        Destroy(other.gameObject);
        Destroy(gameObject); //the asteriod and all its children
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
