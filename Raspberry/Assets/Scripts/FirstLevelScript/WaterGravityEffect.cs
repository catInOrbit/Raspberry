using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGravityEffect : MonoBehaviour
{

    /* The replacement for bouyancy effect, script will effect objects gravity scale instead of using bouyancy components*/
    public float waterGravityScale;
	
	void Update ()
    {
		
	}

    void OnTriggerStay2D(Collider2D objects)
    {
        objects.gameObject.GetComponent<Rigidbody2D>().gravityScale = waterGravityScale;
    }
}
