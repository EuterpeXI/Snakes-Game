using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject panel;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player"){
            panel.gameObject.SetActive(true);
        }
        else{
            panel.gameObject.SetActive(false);
        }
    }
}
