using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
    public static int PlayerHealth;
    public int StartHealth = 20;
    public Text HealthText;
    public Text GOTxt;
    public static int workCampAmount;
    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        PlayerHealth = StartHealth;
        HealthText.text = PlayerHealth.ToString();
        workCampAmount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        HealthText.text = PlayerHealth.ToString();

        if (PlayerHealth == 0)
        {
            GOTxt.text = "Fuck You Noob";
            Time.timeScale = 0;
        }
    }
}
