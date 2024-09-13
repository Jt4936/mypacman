using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperDot : MonoBehaviour {
    public PacstudentMove m_pacstudent;
    // Use this for initialization

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.CompareTo("Player") == 0)
        {
            GameManager.Instant.AddScore(10);
            m_pacstudent.ChangeState(PacstudentMove.Pacstudent_Invicible);
            Destroy(gameObject);
        }
    }

}
