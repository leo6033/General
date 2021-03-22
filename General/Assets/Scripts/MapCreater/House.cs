using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public float MaxHp;

    private float Hp;
    private void Awake()
    {
        Hp = MaxHp;
    }

    public void beAttacked(float damage)
    {
        Hp -= damage;
        if(Hp <= 0)
        {
            beDestroyed();
        }
    }

    private void beDestroyed()
    {
        GetComponent<Renderer>().material.color = Color.red;
        this.enabled = false;
    }


    //测试代码
    //private float timer;
    //private void Update()
    //{
    //    if(timer < 5)
    //    {
    //        timer += Time.deltaTime;
    //    }
    //    else
    //    {
    //        beDestroyed();
    //    }
    //}
}
