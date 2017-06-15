using UnityEngine;
using System.Collections;

public class NPC_Spawner : MonoBehaviour
{
    public GameObject[] npcPrefabs;

    public void initialiseNpcWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomNpc = Random.Range(0, npcPrefabs.Length);
            GameObject tempTile = (Instantiate(npcPrefabs[randomNpc], new Vector3(0,0,0), Quaternion.identity) as GameObject);
            tempTile.transform.parent = this.transform;
        }
    }

}
