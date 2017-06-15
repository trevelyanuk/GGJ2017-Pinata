using UnityEngine;
using System.Collections;

public class MoneyController : MonoBehaviour
{
    private GameObject trump;
	// Use this for initialization
	void Start ()
    {
        trump = GameObject.FindWithTag("Trump");
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider.gameObject.tag);

        if(col.collider.gameObject.CompareTag("Trump"))
        {
            Destroy(gameObject);
        }
    }

	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        transform.position = Vector3.Lerp(transform.position, trump.transform.position, 0.1f);
    }
}
