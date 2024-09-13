using UnityEngine;

public class Maze : MonoBehaviour
{
    public int width;
    public int height;
    public bool[,] walls;

    public Maze(int width, int height)
    {
        this.width = width;
        this.height = height;
        walls = new bool[width,height];
    }

    public bool IsWall(int x, int y)
    {
        return walls[x, y];
    }
}
