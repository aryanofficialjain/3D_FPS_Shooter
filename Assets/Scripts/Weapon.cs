using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public Camera PlayerCamera;

   public bool isShooting, ReadytoShoot;
   public float shootingDelay = 2f;
   bool allowReset = true;

//    reloading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;



//    burst

    public int bulletperBurst = 3;
    public int BurstBulletsLeft;
    public float spreadIntensity;


    // textmesh pro 

    




    // bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;

    public float bulletPrefabLifeTime = 3f;

    public enum ShootingMode{
        Single, 
        Burst,
        Auto
    }

    private Animator animator;

    public ShootingMode currentShootingMode;

    // effects 
    public GameObject muzzleEffects;


    private void Awake() {
        animator = GetComponent<Animator>();
        ReadytoShoot = true;
        BurstBulletsLeft = bulletperBurst;


        bulletsLeft = magazineSize;

    }

   void Update() {
        if(currentShootingMode == ShootingMode.Auto){
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if(currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst ){
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);

        } 

        if(bulletsLeft == 0 && isShooting){
            SoundManager.Instance.emptyMagazine.Play();
        }


        if(ReadytoShoot && isShooting && bulletsLeft > 0){
            BurstBulletsLeft = bulletperBurst;
            FireWeapon();
        }  

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false){
            Reload();
        }

        if(AmmoManager.Instance.ammoDisplay != null){
           AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft/bulletperBurst}/{magazineSize/bulletperBurst}";

        }


    }



    private void FireWeapon()
    {
        bulletsLeft--;
        muzzleEffects.GetComponent<ParticleSystem>().Play();
        ReadytoShoot = false;

        animator.SetTrigger("RECOIL");

        SoundManager.Instance.AKM.Play();



        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;


        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity );

        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if(allowReset){
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        if(currentShootingMode == ShootingMode.Burst && BurstBulletsLeft > 1){
            BurstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }




    }

    private void Reload(){
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
        SoundManager.Instance.ReloadingSound.Play();
    }

    private void ReloadCompleted(){
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    public Vector3 CalculateDirectionAndSpread(){
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit)){
            targetPoint = hit.point;
        }
        else {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x,y,0);
        


    }

    private void ResetShot(){
        ReadytoShoot = true;
        allowReset = true;
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletPrefabLifeTime)
    {
        yield return new WaitForSeconds(bulletPrefabLifeTime);
        Destroy(bullet);
    }
}
