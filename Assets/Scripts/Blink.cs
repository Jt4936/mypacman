using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingController : MonoBehaviour
{
    public Material flashingMaterial;  
    public float flashDuration = 1.0f; 
    public float delay = 0.5f;  
    private bool isFlashing = true;
    private bool up = true;

    private void Update()
    {
        if (isFlashing)
        {
            float alpha = 1 - (Time.time * flashDuration)%1.0f;
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, alpha));  
        }

        if (GetComponent<Renderer>().material.color.a <= 0)
        {
            //StartCoroutine(Flash());
            Debug.Log("=====>222222222222 come into freeze part");
        }
    }

    private IEnumerator Flash()
    {

        isFlashing = false;  
        yield return new WaitForSeconds(delay);  
        isFlashing = true; 
    }
}