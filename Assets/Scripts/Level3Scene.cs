using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Scene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Application.LoadLevel("Level 3");
    }
}
