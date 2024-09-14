using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int m_direction;
    public int m_up = 0;
    public int m_down = 1;
    public int m_left = 2;
    public int m_right = 3;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        print(1);
        switch (m_direction)
        {
            case 0:
                PacStudent.m_PacstudentMoveState = PacStudent.MOVE_UP;
                break;
            case 1:
                PacStudent.m_PacstudentMoveState = PacStudent.MOVE_DOWN;
                break;
            case 2:
                PacStudent.m_PacstudentMoveState = PacStudent.MOVE_LEFT;
                break;
            case 3:
                PacStudent.m_PacstudentMoveState = PacStudent.MOVE_RIGHT;
                break;

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PacStudent.m_PacstudentMoveState = PacStudent.MOVE_NONE;
    }
}
