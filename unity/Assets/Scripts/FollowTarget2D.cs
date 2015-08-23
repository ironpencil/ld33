using UnityEngine;
using System.Collections;

public class FollowTarget2D : MonoBehaviour {

    public Transform target;

    public Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (target == null) { return; }

        Vector3 position = new Vector3(target.position.x, target.position.y, transform.position.z);

        position.x += offset.x;
        position.y += offset.y;
        position.z += offset.z;

        transform.position = position;
	}
}
