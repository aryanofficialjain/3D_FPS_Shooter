using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision ObjectweHit) {

        if(ObjectweHit.gameObject.CompareTag("Target")){
            print("Hit" + ObjectweHit.gameObject.name + " !!!!!" );
            CreateBulletImpactEffect(ObjectweHit);

            Destroy(gameObject);
        }
        
    }

    void CreateBulletImpactEffect(Collision ObjectweHit)
    {
        ContactPoint contact = ObjectweHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalRefrence.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(ObjectweHit.gameObject.transform);
    }
}
