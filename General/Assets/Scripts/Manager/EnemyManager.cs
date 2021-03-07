using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public GameObject m_Instance;

    public void Setup(List<GameObject> housePointList, out List<StateController> stateControllers)
    {
        stateControllers = new List<StateController>(m_Instance.GetComponentsInChildren<StateController>());
        foreach (StateController stateController in stateControllers)
        {
            stateController.SetupAI(true, housePointList);
        }
    }
}
