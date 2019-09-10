using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour {
    public GameObject UI;
    private TowerStats target;
    private float PricePlusUpgrade;
    public GameObject PotionUI;

    //TODO
    //Get Tower Price and add it to calculation in  PricePlusUpgrade and OnSell funciton
    public void SetTarget(TowerStats _target)
    {
        Debug.Log("Set Target");
        target = _target;

        transform.position = target.GetBuildPosition();
        UI.SetActive(true);
    }
    private void Start()
    {
        UI.SetActive(false);
        PotionUI.SetActive(false);
    }
    public void Hide()
    {
        UI.SetActive(false);
        PotionUI.SetActive(false);
    }   
    public void OnUpgrade()
    {
        
        if (target.cost<= PlayerStats.Money)
        {
            PlayerStats.Money = PlayerStats.Money- target.cost;
            PricePlusUpgrade = target.cost + target.TowerCost;
            Instantiate(target.Next_Upgrade, transform.position, transform.rotation);
            Destroy(target.gameObject);
        }
 
    }
    public void OnSell()
    {
       
        float income = (PricePlusUpgrade + target.TowerCost) / 100 * 70;
        
        PlayerStats.Money = PlayerStats.Money + income;
        Destroy(target.gameObject);
        UI.SetActive(false);
    }
    public void OnPotion()
    {
       
        if (PotionUI.activeSelf == true)
        {
            PotionUI.SetActive(false);
        }
        else
        {
            PotionUI.SetActive(true);
        }
        
    }
    public void PotionSelected()
    {
        PotionUI.SetActive(false);
        UI.SetActive(false);
    }
}
