using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public GameObject m_Instance;

    public void Setup(List<GameObject> housePointList, ref List<StateController> stateControllers)
    {
        List<StateController> tmp = new List<StateController>(m_Instance.GetComponentsInChildren<StateController>());
        foreach (StateController stateController in tmp)
        {
            stateController.SetupAI(true, housePointList);
            stateControllers.Add(stateController);
        }
    }


}
