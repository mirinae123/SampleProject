using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject projPrefab;
    public Transform playerPos;

    private float cooldown;
    private List<GameObject> pool;

    void Start()
    {
        pool = new List<GameObject>();
    }

    void Update()
    {
        if (!Player.isAlive) return;

        cooldown -= Time.deltaTime;

        if(AttackSystem.currentWeapon != AttackSystem.Weapon.Ranged || cooldown > 0) return;

        if(Input.GetMouseButtonDown(0))
        {
            CreateProjectile();
            AudioManager.instance.PlaySfx(AudioManager.SFX.Item);
        }
    }

    void CreateProjectile()
    {
        int index = -1;

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            GameObject projectile = Instantiate(projPrefab);
            projectile.transform.position = playerPos.position;
            projectile.transform.parent = transform;
            projectile.GetComponent<RangedProjectile>().lifetime = 1f;
            pool.Add(projectile);

            Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Vector3 dir = (mousePos - playerPos.position).normalized;
            float degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            projectile.transform.rotation = Quaternion.Euler(0, 0, degree);
            projectile.GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
        }
        else
        {
            pool[index].SetActive(true);
            pool[index].transform.position = playerPos.position;
            pool[index].GetComponent<RangedProjectile>().lifetime = 1f;

            Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Vector3 dir = (mousePos - playerPos.position).normalized;
            float degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            pool[index].transform.rotation = Quaternion.Euler(0, 0, degree);
            pool[index].GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
        }
    }
}
