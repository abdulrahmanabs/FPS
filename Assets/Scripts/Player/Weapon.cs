
using System.Collections;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] float shootingDistance = 8;
    [SerializeField] Transform shootingPoint;
    [SerializeField] WeaponData weaponData;
    [SerializeField] ParticleSystem shootingEffect;
    [SerializeField] GameObject shootHitParticle;
    RaycastHit hit;

    CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        cameraShake = GetComponentInParent<CameraShake>();
        weaponData.currentAmmo = weaponData.maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void Shoot()
    {

        ProcessRaycast();
        shootingEffect.Play();
        cameraShake.Shake();
        PopupText.Instance.ShowPopup(hit.point, weaponData.damage.ToString(), Color.red,.2f,transform.gameObject);

    }

    private void ProcessRaycast()
    {
        if (weaponData.currentAmmo <= 0)
            return;
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        if (Physics.Raycast(shootingPoint.position, transform.forward, out hit))
        {


            EnemyHealth enemyHealth;
            if (!hit.transform.gameObject.TryGetComponent<EnemyHealth>(out enemyHealth))
                return;
            enemyHealth.ChangeHealth((int)-weaponData.damage);
            weaponData.currentAmmo--;

            ShowImpactParticle(hit);
        }
    }
    private void ShowImpactParticle(RaycastHit hit)
    {
        GameObject partilce = ObjectPoolManager.Instance.GetObject(shootHitParticle);
        partilce.transform.position = hit.point;
        partilce.transform.rotation = Quaternion.LookRotation(hit.normal);
        partilce.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DespawnObject(obj: partilce, time: .5f));
    }

    private IEnumerator DespawnObject(GameObject obj, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        obj.SetActive(false);
    }
}
