using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
    public static GameObject m_Restart;
    
    
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
