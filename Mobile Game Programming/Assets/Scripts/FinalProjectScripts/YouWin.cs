using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class YouWin : MonoBehaviour
{
    public GameObject youWinScreen;
    // Start is called before the first frame update
    void Start()
    {
        youWinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            youWinScreen.SetActive(true);
        }
    }
}
