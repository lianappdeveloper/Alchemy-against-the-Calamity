using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;


    //U cant have more then 1 build manager
    private void Awake()
    {
        if (instance!= null)
        {
            Debug.Log("u cant have more then 1 buildmanager");
            return;
        }
        instance = this;
    }

    public GameObject Archery;
    public GameObject Treb;
    public GameObject Workamp;
    public GameObject Barracks;
    public GameObject chimimcalthrower;
    public GameObject HuntersLookOut;



    private TurretBlueprint turretToBuild;
    private TowerStats SelectedNode;
    public NodeUI NodeUI;
    private TowerStats TowerStats;
    

   // if u can or cant build
   public bool CanBuild { get { return turretToBuild != null;}}
    
    //build turret on the tower node
    public void BuildTurretOn(TowerNode TowerNode)
    {
        if (PlayerStats.Money<turretToBuild.cost)
        {
            Debug.Log("Not Enough Money To Build That Turret");
            return;
        }
        else
        {
            PlayerStats.Money -= turretToBuild.cost;

            GameObject turret = Instantiate(turretToBuild.prefab, TowerNode.GetBuildPosition(), Quaternion.identity);
            TowerNode.turret = turret;
        }


    }
    public void SelectNode(TowerStats TowerStats)
    {
        if (SelectedNode== TowerStats)
        {
            DeselectNode();
            return;
        }
        SelectedNode = TowerStats;
        turretToBuild = null;
        Debug.Log("Selected Mode");
        NodeUI.SetTarget(TowerStats);

    }
    public void DeselectNode()
    {
        SelectedNode = null;
        NodeUI.Hide();
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }


}
