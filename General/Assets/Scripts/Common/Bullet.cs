using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource audioSource;

    public float speed = 5.0f;
    [HideInInspector] public Stats stats;
    private bool attack = false;

    public IEnumerator shoot(Vector3 targetPosition)
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        while (!attack)
        {
            //朝向目标  (Z轴朝向目标)
            this.transform.LookAt(targetPosition);
            //根据距离衰减 角度
            float angle = Mathf.Min(1, Vector3.Distance(this.transform.position, targetPosition) / distanceToTarget) * 45;
            //旋转对应的角度（线性插值一定角度，然后每帧绕X轴旋转）
            this.transform.rotation = this.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
            //当前距离目标点
            float currentDist = Vector3.Distance(this.transform.position, targetPosition);
            if (currentDist < 0.5f)
            {
                attack = true;
            }
            //平移 （朝向Z轴移动）
            this.transform.Translate(Vector3.forward * Mathf.Min(speed * Time.deltaTime, currentDist));
            yield return null;
        }

        StartCoroutine(delete());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tag != other.tag)
            attack = true;
        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != null && attack)
        {
            StartCoroutine(targetHealth.TakeDamage(CalculateDamage(other), 0, audioSource));
        }

        if(attack)
            StartCoroutine(delete());
    }

    private float CalculateDamage(Collider collider)
    {
        Stats myStats = stats;
        if (collider.tag == "House" || collider.tag == "Castle")
            return myStats.physicalAttack + myStats.magicAttack;
        Stats enemyStats = collider.GetComponent<StateController>().stats;

        float physical = myStats.physicalAttack - enemyStats.physicalDefense;
        physical = physical > 0 ? physical : 0;
        float magic = myStats.magicAttack - enemyStats.magicDefense;
        magic = magic > 0 ? magic : 0;
        float harm = physical + magic;

        int X = Random.Range(0, 100);

        if (X <= enemyStats.dodgeRate)
            harm = 0;
        else if (X <= myStats.critRate + enemyStats.dodgeRate)
            harm = harm * myStats.critHarm;
        Debug.Log("Bullet take " + harm);

        return harm;
    }

    private IEnumerator delete()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
