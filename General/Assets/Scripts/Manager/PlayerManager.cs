using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public GameObject m_Instance;
    private LegionUtil[] legionUtils;

    public void Setup(ref List<StateController> stateControllers)
    {
        List <StateController> tmp = new List<StateController>(m_Instance.GetComponentsInChildren<StateController>());
        legionUtils = m_Instance.GetComponentsInChildren<LegionUtil>();
        foreach (StateController stateController in tmp)
        {
            stateController.SetupAI(true, null);
            stateController.navMeshAgent.isStopped = true;
            stateControllers.Add(stateController);
        }
        
    }

    public void setColor(Color color)
    {
        foreach (LegionUtil legionUtil in legionUtils)
        {
            legionUtil.setColor(color);
        }
    }
}
