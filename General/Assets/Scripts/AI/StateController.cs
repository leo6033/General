﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;
    public Stats stats;
    public bool m_IsRemote;
    public string Types;
    public List<AudioClip> AttackAudios;
    public AudioClip WalkAudio;

    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Attack attack;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public List<GameObject> houseList;
    [HideInInspector] public GameObject targetHouse;
    [HideInInspector] public Vector3 targetPoint;
    [HideInInspector] public int nextPoint;
    [HideInInspector] public Collider attackObject;
    [HideInInspector] public Vector3 RelativePosition;
    [HideInInspector] public Animator animator;
    private SpriteRenderer aperture;

    private bool aiActive = true;

    private void Awake()
    {
        aperture = GetComponentInChildren<SpriteRenderer>();
        aperture.enabled = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        attack = GetComponent<Attack>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetupAI(bool aiActivationFromManager, List<GameObject> HouseFromManager)
    {
        
        houseList = HouseFromManager;
        aiActive = aiActivationFromManager;
        navMeshAgent.enabled = aiActive;

        GetComponent<Health>().Init(stats.HP);

        //Debug.Log(gameObject.name + transform.parent.transform.position + GetComponent<Transform>().position);
        targetPoint = transform.position;
        RelativePosition = transform.parent.transform.position - GetComponent<Transform>().position;

        float distance = Mathf.Infinity;
        if (houseList == null)
            return;
        foreach (GameObject house in houseList)
        {
            float tmpDistance = Vector3.Distance(transform.parent.position, house.transform.position);
            if (tmpDistance < distance)
            {
                distance = tmpDistance;
                targetHouse = house;
            }
        }
        Debug.Log(transform.gameObject.name + " setup" + targetHouse.transform.position);
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            //Debug.Log(transform.gameObject.name + " start" + nextState);
            animator.SetInteger("walk", 0);
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckifCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    public void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }

    public void OnSelect()
    {
        aperture.enabled = true;
    }

    public void OnUnselect()
    {
        aperture.enabled = false;
    }
}
