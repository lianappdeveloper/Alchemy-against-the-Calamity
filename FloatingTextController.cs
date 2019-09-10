using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    
    private static FloatingText PopUpText,PopUpTextFire,PopUpTextAir,PopUpTextWater,PopUpTextEarth,PopUpTextPoison;
    private static GameObject canvas;


    /*make dmg show up
    int display = Mathf.RoundToInt(amount);
    FloatingTextController.CreateFloatingText(display.ToString(),transform);
        Debug.LogFormat("{0} was dealt {1} damage", gameObject.name, amount);
*/
   
    public static void Initialize()

    {

        canvas = GameObject.Find("DamageCanvas");
     

        if (!PopUpText)
            PopUpText = Resources.Load<FloatingText>("PopUpParent");
        if (!PopUpTextFire)
            PopUpTextFire = Resources.Load<FloatingText>("PopUpParentFire");
        if (!PopUpTextAir)
            PopUpTextAir = Resources.Load<FloatingText>("PopUpParentAir");
        if (!PopUpTextWater)
            PopUpTextWater = Resources.Load<FloatingText>("PopUpParentWater");
        if (!PopUpTextEarth)
            PopUpTextEarth = Resources.Load<FloatingText>("PopUpParentEarth");
        if (!PopUpTextPoison)
            PopUpTextPoison = Resources.Load<FloatingText>("PopUpParentPoison");
    }


    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(PopUpText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform,false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
    public static void CreateFloatingTextFire(string text, Transform location)
    {
        FloatingText instance = Instantiate(PopUpTextFire);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
    public static void CreateFloatingTextAir(string text, Transform location)
    {
        FloatingText instance = Instantiate(PopUpTextAir);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
    public static void CreateFloatingTextWater(string text, Transform location)
    {
        FloatingText instance = Instantiate(PopUpTextWater);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
    public static void CreateFloatingTextEarth(string text, Transform location)
    {
        FloatingText instance = Instantiate(PopUpTextEarth);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
    public static void CreateFloatingTextPoison(string text, Transform location)
    {
        FloatingText instance = Instantiate(PopUpTextPoison);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}
