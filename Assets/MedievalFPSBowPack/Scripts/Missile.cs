using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Missile : MonoBehaviour
{
	#region Fields

	[HideInInspector]
	public Vector3 directionFrom = Vector3.zero;
	[HideInInspector]
	private int hitCount = 0;
	private float damage;
	private float maxHits;
	private float impactForce; 
	private float maxInaccuracy;     
	private float speed;    
	private float lifetime = 5f;  
	[HideInInspector]
	public string gunName = "";
	private Vector3 velocity = Vector3.zero;
	private Vector3 newPos = Vector3.zero;  
	private Vector3 oldPos = Vector3.zero;  
	private bool hasHit = false;            
	private Vector3 direction;             
	[Space(5)]
	public AudioSource AudioReferences;
	[SerializeField]private AudioClip concreteSound;
	[SerializeField]private AudioClip genericSound;
	public Vector2 mPicht = new Vector2(1.0f, 1.5f);

	[HideInInspector]
	public enum HitType
	{
		CONCRETE,
		GENERIC
	};

	HitType type;

	public GameObject missileHitObject;

	#endregion


	public void SetUp (MissileInfo info) // information sent from gun to bullet to change bullet properties
	{
		damage = info.damage;
		impactForce = info.impactForce;
		maxHits = info.maxPenetration;             // max number of bullet impacts before bullet is destroyed
		maxInaccuracy = info.maxspread;
		speed = info.speed;
		directionFrom = info.position;
		direction = transform.TransformDirection (1f, Random.Range(-maxInaccuracy, maxInaccuracy), Random.Range(-maxInaccuracy, maxInaccuracy));
		newPos = transform.position;
		oldPos = newPos;     
		velocity = speed * transform.right;

		Destroy (gameObject, lifetime);
	}
		
	Vector3 dir = Vector3.zero;

	void Update()
	{
		if (hasHit)
			return;
		
		// assume we move all the way
		newPos += (velocity + direction) * Time.deltaTime;

		// Check if we hit anything on the way
		dir = newPos - oldPos;
		float dist = dir.magnitude;

		if (dist > 0)
		{
			// normalize
			dir /= dist;

			RaycastHit[] hits = Physics.RaycastAll (oldPos, dir, dist);

			Debug.DrawLine (oldPos, newPos, Color.red);

			for (int i = 0; i < hits.Length; i++)
			{
				RaycastHit hit = hits[i];

				newPos = hit.point;

				OnHit(hit);

				if (hitCount >= maxHits)
				{
					hasHit = true;
					Destroy(gameObject);                    
				}
			}
		}

		oldPos = transform.position;
		transform.position = newPos;
	}
		

	void OnHit(RaycastHit hit)
	{        
		GameObject go = null;

		Ray mRay = new Ray (transform.position, transform.forward);

		if (hit.rigidbody != null && !hit.rigidbody.isKinematic) // if we hit a rigi body... apply a force
		{
			float mAdjust = 1.0f / (Time.timeScale * (0.02f / Time.fixedDeltaTime));
			hit.rigidbody.AddForceAtPosition(((mRay.direction * impactForce) / Time.timeScale) / mAdjust, hit.point);
		}

		switch (hit.transform.tag) // decide what the bullet collided with and what to do with it
		{
			case "Environment(Wall, Ground, etc.)":
				hitCount += 2; // add 2 hits to counter... concrete is hard
				type = HitType.CONCRETE;
				if (hitCount >= maxHits)
					StickToTheWall (hit, hit.transform.gameObject);
				break;
			case "Enemy":
				hitCount += 2; // add 2 hits to counter... concrete is hard
				type = HitType.CONCRETE;
				if (hitCount >= maxHits)
					StickToTheWall(hit, hit.transform.gameObject);
				break;
			case "Generic" :
				hitCount++;
				type = HitType.GENERIC;
			if (hitCount >= maxHits)
				StickToTheWall (hit, hit.transform.gameObject);
				break;
			case "Invisible": 	//do nothing
			case "Projectile": 	// do nothing if 2 bullets collide
			default:
				break;
		}
	}

	void StickToTheWall (RaycastHit hit, GameObject go)
	{
		Vector3 contact = hit.point;

		GameObject newMissiletHit = Instantiate (missileHitObject, contact, this.transform.rotation) as GameObject;

//		newMissiletHit.transform.parent = go.transform;
	}
}