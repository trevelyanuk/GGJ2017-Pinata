using UnityEngine;
using System.Collections;

public class Touchy : MonoBehaviour {

    public Vector2 target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        target = Input.mousePosition;
        Debug.Log("position is " + target.ToString());
    }
}
