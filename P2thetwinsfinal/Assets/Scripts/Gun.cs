using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public int maxAmmo = 30;
    public int bulletsPerTap = 1;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public TextMeshProUGUI ammoText;

    private int currentAmmo;
    private bool canShoot = true;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
        {
            // Out of ammo
            return;
        }

        muzzleFlash.Play();

        currentAmmo -= bulletsPerTap;
        UpdateAmmoText();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }

        if (currentAmmo <= 0)
        {
            // Out of ammo after this shot
            canShoot = false;
        }

        if (currentAmmo <= 0 && !canShoot)
        {
            Invoke(nameof(EnableShooting), 1f / fireRate);
        }
    }

    private void EnableShooting()
    {
        canShoot = true;
    }


    private void Reload()
    {
        int ammoToReload = maxAmmo - currentAmmo;
        currentAmmo += ammoToReload;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        ammoText.SetText(currentAmmo + " / " + maxAmmo);
    }
}
