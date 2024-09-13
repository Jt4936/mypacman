using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreMgr : MonoBehaviour {

    public TextMeshProUGUI m_durationText;
    public TextMeshProUGUI m_highScoreText;
 

	// Use this for initialization
	void Start () {
        //TextMeshPro text = textMeshPro.GetComponent<TextMeshPro>();
        //text.text = "hello, world!";
         
    }
	
	// Update is called once per frame
	void Update () {
        m_durationText.text = "Total Time：" + GameManager.m_duration;
        m_highScoreText.text = "Highest Score: " + GameManager.m_highScore;
    }
}
