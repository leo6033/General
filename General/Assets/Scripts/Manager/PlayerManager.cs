using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public GameObject m_Instance;

    public void Setup()
    {
        StateController[] stateControllers = m_Instance.GetComponentsInChildren<StateController>();
        foreach(StateController stateController in stateControllers)
        {
            stateController.SetupAI(true, null);
            stateController.navMeshAgent.isStopped = true;
        }
    }
}
