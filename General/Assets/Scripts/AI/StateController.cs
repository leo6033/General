using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;
    public Stats stats;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Attack attack;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public List<Transform> pointList;
    //[HideInInspector] public Transform targetPoint;
    public Transform targetPoint;
    [HideInInspector] public int nextPoint;
    [HideInInspector] public Collider attackObject;

    private bool aiActive = true;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        attack = GetComponent<Attack>();
    }

    public void SetupAI(bool aiActivationFromManager, List<Transform> PointsFromManager)
    {
        pointList = PointsFromManager;
        aiActive = aiActivationFromManager;
        navMeshAgent.enabled = aiActive;
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
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

}
