using UnityEngine;

public class FlyEnemy : EnemyController {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, Mathf.Abs(speed) * Time.deltaTime);
        }
    }
}
