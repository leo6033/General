using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> transforms;
    [HideInInspector] public GameObject m_Instance;

    public void Init(float Number, GameObject LegionPrefeb)
    {
        for(int i = 0; i < Number; i++)
        {
            Instantiate(LegionPrefeb, transforms[i].position, transforms[i].rotation, transforms[i]);
        }
    }

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
