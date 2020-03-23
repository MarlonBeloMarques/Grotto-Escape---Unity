using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : EnemyController {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
	}

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rig2d.velocity = new Vector2(speed, rig2d.velocity.y);
        }
        else
        {
            rig2d.velocity = new Vector2(0, rig2d.velocity.y);
        }
    }
}
