using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
    public static GameObject m_Restart;
   public virtual void  OnTriggerEnter2D(Collider2D collider)
   {
       if (collider.tag == "Player")
       {
           int pacstudentState =  collider.transform.GetComponent<PacstudentMove>().m_pacstudentState;
           if (pacstudentState == PacstudentMove.Pacstudent_Normal)
           {
               //m_Restart.SetActive(true);
               //Destroy(collider.gameObject);
               PacstudentMove.m_life--;
               if (PacstudentMove.m_life <= 0)
               {
                   GameManager.Instant.SaveHighScore();
                   WinCondiction.Instant.Death();
                   m_Restart.SetActive(true);
                   Destroy(collider.gameObject);
               }
               else
               {
                   collider.GetComponent<PacstudentMove>().ChangeState(PacstudentMove.Pacstudent_Hurt);
               }
           }
           else if (pacstudentState == PacstudentMove.Pacstudent_Invicible)
           {
               GameManager.Instant.AddScore(100);
               GameManager.Instant.GhostRevenge(gameObject);
               gameObject.SetActive(false);
           }
           if (pacstudentState == PacstudentMove.Pacstudent_Hurt)
           {
               
           }
           
       }

   }

    
    public Transform EscapeToNearWayPoint()
   {
       GameObject[] wayPoints =  GameObject.FindGameObjectsWithTag("WayPoint");
       Transform nearWay = transform;
       float distance = float.MaxValue;
       foreach (var item in wayPoints)
       {
           if (Vector2.Distance(item.transform.position,transform.position) < distance)
           {
               distance = Vector2.Distance(item.transform.position, transform.position);
               nearWay = item.transform;
           }
       }
       return nearWay;
   }

    public Transform ChangeFarWayPoint(Transform pacstudent,Transform nowPosition)
    {
        Transform nextWay = transform;
        float distance = 0;
        WayPoint nextWays = nowPosition.GetComponent<WayPoint>();
        foreach (Transform wayPoint in nextWays.m_nextPoint)
        {
            if (Vector2.Distance(wayPoint.position, pacstudent.position) > distance)
            {
                distance = Vector2.Distance(wayPoint.position, pacstudent.position);
                nextWay = wayPoint;
            }
        }
        return nextWay;
    }
}
