using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private PropellerShipBehaviour PSBscript;

    [Header("Machineguns")]
    public Transform Blaster;
    public GameObject Bullet;
    public float BulletSpeed;
    public float fireRate = 15f;
    public float spreadFactor;
    private float nextTimeToFire = 0f;

    [Header("Missiles")]
    public Transform MissileLaucherTransform;
    public GameObject Missile;

    [Header("Targeting")]
    public LayerMask ennemiLayer;
    public float detectionRange = 50f;
    public List<GameObject> TargetsInSight = new List<GameObject>();
    private int whereInList = 0;
    public GameObject LockedShip;
    public GameObject lockedWidget;
    public GameObject GizmoLock;

    private void Start()
    {
        PSBscript = GetComponent<PropellerShipBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //shooting with autocannon
        if (Input.GetButton("Abutton") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            shootAutocannon(Bullet, Blaster, BulletSpeed);
        }

        //shooting a missile
        if (Input.GetButtonDown("Bbutton"))
        {
            //cooldownMissile
            shootMissile(Missile, MissileLaucherTransform);
        }

        //lock on an object from the target list
        if (Input.GetButtonDown("Ybutton"))
        {
            LockTarget();
        }

        //add cursor to locked ship and keep it facing Player
        if (LockedShip != null)
        {
            lockedWidget.transform.LookAt(transform);
            //GizmoLock.transform.LookAt(LockedShip.transform);

            lockedWidget.transform.position = LockedShip.transform.position;
            lockedWidget.SetActive(true);
        }
        else
        {
            lockedWidget.SetActive(false);
            lockedWidget.transform.position = new Vector3(0, 0, 0);
        }
    }

    void shootAutocannon(GameObject bullet, Transform firepoint, float BulletSpeed)
    {
        Vector3 shootDir = firepoint.transform.forward;
        shootDir.x += Random.Range(-spreadFactor, spreadFactor);
        shootDir.y += Random.Range(-spreadFactor, spreadFactor);

        var projectileObj = Instantiate(bullet, firepoint.position, Blaster.rotation) as GameObject;
        projectileObj.GetComponent<Rigidbody>().AddForce(shootDir * (BulletSpeed + PSBscript.Speed), ForceMode.Impulse);
    }

    void shootMissile(GameObject missile, Transform MissileLauncher)
    {
        var projectileObj = Instantiate(missile, MissileLauncher.position, MissileLauncher.rotation) as GameObject;
        if(PSBscript.Speed > 0)
        {
            projectileObj.GetComponent<Missile>().speed += PSBscript.Speed;
        }
        if (LockedShip != null)
        {
            projectileObj.GetComponent<Missile>().target = LockedShip.transform;
        }
        projectileObj.GetComponent<Missile>().initiateTrackingDelay();
    }

    void LockTarget()
    {
        //iterate trough target list
        whereInList++;
        if (TargetsInSight.Count != 0)
        {
            if (whereInList <= TargetsInSight.Count - 1)
            {
                if (TargetsInSight[whereInList] != null)
                {
                    LockedShip = TargetsInSight[whereInList];
                }
                else if (TargetsInSight[whereInList] == null)
                {
                    TargetsInSight[whereInList] = TargetsInSight[TargetsInSight.Count - 1];
                    TargetsInSight.RemoveAt(TargetsInSight.Count - 1);
                }
            }
            else if (whereInList > TargetsInSight.Count - 1)
            {
                whereInList = 0;
                if (TargetsInSight[whereInList] != null)
                {
                    LockedShip = TargetsInSight[whereInList];
                }
                else if (TargetsInSight[whereInList] == null)
                {
                    TargetsInSight[whereInList] = TargetsInSight[TargetsInSight.Count - 1];
                    TargetsInSight.RemoveAt(TargetsInSight.Count - 1);
                }
            }
        }

        //unlock
        if (TargetsInSight.Count == 0)
        {
            LockedShip = null;
        }
    }
}
