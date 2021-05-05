using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    protected InputHandler input;

    [Header("Components")]
    public Camera mainCam;
    public Transform muzzle;
    public Transform sight;

    [Header("Combat Stats")]
    public float damage = 10f;
    public float range = 100f;
    public float adsSpeed = 8f;

    [Header("Variables")]
    public ParticleSystem muzzleFlash;
    public GameObject bulletTrail;
    public GameObject bloodSplat;
    protected AudioSource audioSource;

    [Header("ADS Positioning")]
    public Vector3 originalPosition;
    public Vector3 aimPosition;
    public GameObject hipRetical;

    [Header("Recoil Stats")]
    public float hipBloom;
    public float vRecoil;
    public float hRecoil;
    public float kickback;
    protected Quaternion originalRotation;

    [Header("Operational Info")]
    public bool canFire;

    protected void Init()
    {
        input = InputHandler.instance;
        audioSource = GetComponent<AudioSource>();

        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    protected abstract void CheckForFire();

    

    protected void Shoot()
    {
        if (input.aimDown)
        {
            ADSShoot();
        }
        else
        {
            HipShoot();

            
        }
    }

    private void ADSShoot()
    {

        audioSource.Play();
        muzzleFlash.Play();
        Instantiate(bulletTrail, muzzle.position, muzzle.rotation);

        RaycastHit hit;

        if (Physics.Raycast(sight.position, sight.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject temp = Instantiate(bloodSplat, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(temp, 2);
        }

        transform.Rotate(-vRecoil, hRecoil, 0);
        transform.position -= transform.forward * kickback;



    }

    private void HipShoot()
    {
        Vector3 calcBloom = mainCam.transform.position + mainCam.transform.forward * range;
        calcBloom += Random.Range(-hipBloom, hipBloom) * mainCam.transform.up;
        calcBloom += Random.Range(-hipBloom, hipBloom) * mainCam.transform.right;
        calcBloom -= mainCam.transform.position;
        calcBloom.Normalize();

        audioSource.Play();
        muzzleFlash.Play();
        Instantiate(bulletTrail, muzzle.position, Quaternion.LookRotation(calcBloom));

        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, calcBloom, out hit, range))
        {
            Debug.Log("Hit something");
        }

        transform.Rotate(-vRecoil, hRecoil, 0);
        transform.position -= transform.forward * kickback;
    }

    protected void AimDownSights()
    {
        if (input.aimDown)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * adsSpeed );
        } else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * adsSpeed);
        }
    }

    protected void ResetPosition()
    {
        if (!input.aimDown)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 4f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, Time.deltaTime * 4f);
        } else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * 4f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, Time.deltaTime * 4f);
        }
    }

}
