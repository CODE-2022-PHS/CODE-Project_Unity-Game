using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//using UnityStandardAssets.Characters.FirstPerson;



public class BowControlLegacy : WeaponsBaseLegacy
{
	#region Fields

	[SerializeField] bool useSmooth = true;
	[Range (1f, 10f)]
	[SerializeField] float aimSmooth;
	[Range(1, 179)]
	[SerializeField] float aimFov = 35;
	public float defaultFov;
	public float currentFov;

	[HideInInspector] public float baseSpread = .25f;

	public UIArrow canUse;
	public float maxSpread = 1.0f; 
	private float defaultSpread;
	private float defaultMaxSpread;

	public GameObject arrow = null;
	public Transform mountPoint = null;    
	public GameObject attachedArrow = null;

	bool isFiring = false;  

	// basic stats

	public int range = 300; 
	public float damage = 20.0f;
	public float maxPenetration = 3.0f;
	public float fireRate = 0.5f; 
	public int impactForce = 50;   
	public float arrowSpeed = 60.0f;  

	bool isLoaded = false;

	#endregion

	protected override void Awake ()
	{
		base.Awake ();
		defaultSpread = baseSpread;
		defaultMaxSpread = maxSpread;
	}	

	protected override void Start ()
	{
		base.Start ();
		defaultFov = Camera.main.fieldOfView;
		
		canAim = true;
	}

	protected override void InputUpdate()
	{
		if(canUse.canUseBow == false)
        {
			canAim = false;
        }
		else if(canUse.canUseBow == true)
        {
			canAim = true;
        }
		if (Input.GetMouseButton (1) && CanAim)
		{
			isAimed = true;

			if (!isFiring)
			{
				EmptySlot(false);
			}


			if (Input.GetMouseButtonDown (0) && CanFire && isLoaded)
			{
				isFiring = true;
				GlobalData.currArrows--;
				StartCoroutine (Fire ());  
			}
		}
		else
		{
			isAimed = false;
			isLoaded = false;
		}
	}

	protected override void SyncState()
	{
		if (isAimed && !isLoaded && !isFiring && !disableWeapon)
		{
			if (playerSync) 
			{
				if (playerSync.WpState != WeaponState.PrepareAiming) 
				{
					playerSync.WpState = WeaponState.PrepareAiming;
					StartCoroutine (LoadArrow ());
				}
			}
		}
		else if (isAimed && isLoaded && !isFiring && !disableWeapon)
		{
			if (playerSync) 
			{
				if (playerSync.WpState != WeaponState.AimedIdle) 
				{
					playerSync.WpState = WeaponState.AimedIdle;
					PlayWeaponAnimation (weaponAnimations.Weapon_ADS_Idle.name, WrapMode.Loop);
				}
			}
		}
		else if (isAimed && isFiring && !disableWeapon)
		{
			if (playerSync) 
			{
				if (playerSync.WpState != WeaponState.AimedFiring) {
					playerSync.WpState = WeaponState.AimedFiring;
				}
			}
		}
		else if (disableWeapon)
		{
			if (playerSync)
			{
				playerSync.WpState = WeaponState.Lowering;
			}
		}
		else if (controller.IsRunning() && !disableWeapon && !isFiring && !isAimed)
		{
			if (playerSync)
			{
				playerSync.WpState = WeaponState.Running;
			}
		}
		else if (controller.IsWalking() && !disableWeapon && !isFiring && !isAimed)
		{
			if (playerSync)
			{
				playerSync.WpState = WeaponState.Walking;
				PlayWeaponAnimation (weaponAnimations.Weapon_Idle.name, WrapMode.Loop);
			}
		}
		else
		{
			if (playerSync) 
			{
				if (playerSync.WpState != WeaponState.Idle) 
				{
					playerSync.WpState = WeaponState.Idle;
					PlayWeaponAnimation (weaponAnimations.Weapon_Idle.name, WrapMode.Loop);
				}
			}
		}
	}

	protected override void Aim()
	{
		if (isAimed)
		{
			currentFov = aimFov;
			baseSpread = defaultSpread / 2f; 
			maxSpread = defaultMaxSpread / 2f;
		}
		else 
		{      
			currentFov = defaultFov;
			baseSpread = defaultSpread;
			maxSpread = defaultMaxSpread;
		}

		Camera.main.fieldOfView = useSmooth ? 
			Mathf.Lerp(Camera.main.fieldOfView, currentFov, Time.deltaTime * (aimSmooth * 3)) : //apply fog distance
			Mathf.Lerp(Camera.main.fieldOfView, currentFov, Time.deltaTime * aimSmooth);
	}



	IEnumerator Fire()
	{
		StartCoroutine (FireOneShot());
		EmptySlot (true);

		isLoaded = false;
		Source.clip = FireSound;
		Source.spread = Random.Range (1.0f, 1.5f);
		Source.pitch  = Random.Range (1.0f, 1.05f);
		Source.Play();
		
		PlayWeaponAnimation (weaponAnimations.Weapon_ADS_Fire.name, WrapMode.Once);
		yield return StartCoroutine (WaitAnimationFinish (weaponAnimations.Weapon_ADS_Fire));
		
		isFiring = false;
		//yield return null;
	}



	IEnumerator FireOneShot()
	{
		Vector3 position = mountPoint.position;
		// set the gun's info into an array to send to the bullet
		MissileInfo info = new MissileInfo();
		info.damage = damage;
		info.impactForce = impactForce;
		info.maxPenetration = maxPenetration;
		info.maxspread = maxSpread;
		info.speed = arrowSpeed;
		info.position = this.transform.root.position;
		info.lifeTime = range;

		Quaternion q = Quaternion.Euler (new Vector3 (0, transform.eulerAngles.y - 90f, -transform.eulerAngles.x));
		GameObject newArrow = Instantiate (arrow, position, q) as GameObject;

		newArrow.GetComponent<Missile>().SetUp(info);
		newArrow.transform.RotateAround (newArrow.transform.position, newArrow.transform.forward, 90f);

		Source.clip = FireSound;
		Source.spread = Random.Range (1.0f, 1.5f);
		Source.Play();

		yield return null;
	}



	void EmptySlot (bool value)
	{
		if(value)
        {
			attachedArrow.SetActive (false);
        }
		else
        {
			attachedArrow.SetActive (true);
        }
	}

	IEnumerator LoadArrow()
	{
		PlayWeaponAnimation (weaponAnimations.Weapon_IdleToADS.name);
		yield return StartCoroutine (WaitAnimationFinish (weaponAnimations.Weapon_IdleToADS));
		isLoaded = true;
	}



	public override IEnumerator ReloadNormal()
	{
		PlayWeaponAnimation (weaponAnimations.Weapon_Reload.name, WrapMode.Once);
		EmptySlot (false);
		yield return StartCoroutine (WaitAnimationFinish (weaponAnimations.Weapon_Reload));
		isReloading = false;
		canAim = true;
		canFire = true;
	}



	public override void DisableWeapon()
	{
		canAim = false;
		isReloading = false;
		attachedArrow.SetActive (false);
		base.DisableWeapon ();
	}



	public bool CanReload
	{
		get
		{
			if (!isReloading) 
			{
				if (controller.IsWalking() || controller.IsQuiet())
                {
					return true;
                }
				return false;
			}
			return false;
		}
	}

		

}	// class