
using UnityEngine;
using UnityEngine.EventSystems;
public class TowerNode : MonoBehaviour {

    public bool IsShopOpen = false;
    //Z position
    public Vector3 positionOffSet;

    //colors
    public Color hoverColor;
    private Color startColor;

    //GameObjects
    public GameObject turret;
    
    //shop
    public Shop shop;
    //tileOption
 //   public TileOptions tileOptions;

    //renderer!!
    private Renderer rend;

    //Build Manager
    BuildManager buildManager;

     void Start()
    {
        rend=GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;

        shop = GameObject.Find("Shop").GetComponent<Shop>();
       // tileOptions = GetComponent<TileOptions>();
       

    }

    //When mouse is on the turret node
    void OnMouseDown()
    {
        
        bool IsShopOpen = true;
         if (IsShopOpen == true)
        {
          
            shop.Enable();
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //if (turret != null)
        //{
        //    Debug.Log("there is turret here");
        //    buildManager.SelectNode(this);
        //    return;
        //}
        if (!buildManager.CanBuild)
            return;

        buildManager.BuildTurretOn(this);
     //if you want to destroy the node after you build a tower on it.
     //   Destroy(this.gameObject);


    }
    //get position
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }

    //when mouse get into the node space
    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }
        
    }
    //when mouse exit the node space
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
