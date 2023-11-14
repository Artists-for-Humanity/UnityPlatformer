using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gm;
    public AudioClip coinSound;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Collected Coin");
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            gameObject.SetActive(false);
            gm.score += 100;
            Destroy(gameObject);
        }

    }
}
