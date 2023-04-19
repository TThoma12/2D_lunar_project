using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public int Health = 6;
    public Image[] hearts;

    public GameObject gameOverScreen;
    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.gameObject.SetActive(false);
        }
        for (int i = 0; i < Health; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
        if (Health == 0)
        {
            gameOverScreen.SetActive(true);
            

        }
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;

    }


}
