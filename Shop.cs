
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    

    public TurretBlueprint Archery;
    public TurretBlueprint Treb;
    public TurretBlueprint Workamp;
    public TurretBlueprint Barracks;
    public TurretBlueprint ChimichalThrower;
    public TurretBlueprint HuntersLookOut;


    public Button Archeryy;
    public Button Trebb;
    public Button WorkCampp;
    public Button ChimichalThrowerr;
    public Button Barrackss;
    public Button HuntersLookOutt;
    public Button Cancel;


    TowerNode TowerNode;
    BuildManager buildManager;

  
    void Start()
    {
        buildManager = BuildManager.instance;
        
        disableAllButtons();

        //Enable();
    }

    public void SelectArchery()
    {
        buildManager.SelectTurretToBuild( Archery);

        

       
       
    }
    public void SelectTreb()
    {
        buildManager.SelectTurretToBuild( Treb);
       

      
    }
    public void SelectWorkamp()
    {
        buildManager.SelectTurretToBuild( Workamp);
        
    }
    public void SelectBarracks()
    {
        buildManager.SelectTurretToBuild(Barracks);
      

    }
    public void SelectChimichalThrower()
    {
        buildManager.SelectTurretToBuild(ChimichalThrower);
        
    }
    public void SelectCancel()
    {
        disableAllButtons();
       
    }
    public void SelectHuntersLookOut()
    {
        buildManager.SelectTurretToBuild(HuntersLookOut);
        
    }


    public void Enable()
    {
        ChimichalThrowerr.gameObject.SetActive(true);
        Archeryy.gameObject.SetActive(true);
        Trebb.gameObject.SetActive(true);
        WorkCampp.gameObject.SetActive(true);
        Barrackss.gameObject.SetActive(true);
        HuntersLookOutt.gameObject.SetActive(true);
        Cancel.gameObject.SetActive(true);

    }

    public void disableAllButtons()
    {
        ChimichalThrowerr.gameObject.SetActive(false);
        Archeryy.gameObject.SetActive(false);
        Trebb.gameObject.SetActive(false);
        WorkCampp.gameObject.SetActive(false);
        Barrackss.gameObject.SetActive(false);
        HuntersLookOutt.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);
        buildManager.SelectTurretToBuild(null);
       
    }
   
}
