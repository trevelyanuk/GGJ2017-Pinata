using UnityEngine;
using System.Collections;

public class FloorSpawner : MonoBehaviour
{
    public GameObject floorTilePrefab;
    // Use this for initialization
    public int width = 8, height = 8;
    void Start()
    {
        genFloor();
    }
    public void genFloor()
    {
        for (int i = 0; i <= width; i++)
        {
            for (int j = 0; j <= height; j++)
            {
                float x = i - (width * 0.5f);
                float z = j - (height * 0.5f);
                Instantiate(floorTilePrefab, new Vector3(x,0,z), Quaternion.identity);
                Debug.Log("Floor Tile coords = "+ x + " 0 " + z);
            }
        }
    }

}
