using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public GameObject m_Instance;

    public void Setup(out List<StateController> stateControllers)
    {
        stateControllers = new List<StateController>(m_Instance.GetComponentsInChildren<StateController>());
        foreach(StateController stateController in stateControllers)
        {
            stateController.SetupAI(true, null);
            stateController.navMeshAgent.isStopped = true;
        }
    }
}
