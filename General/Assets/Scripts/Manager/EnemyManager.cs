using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> transforms;
    public GameObject ensign;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int count;

    private List<StateController> tmp;

    public void Init(float Number, GameObject LegionPrefeb)
    {
        for(int i = 0; i < Number; i++)
        {
            Instantiate(LegionPrefeb, transforms[i].position, transforms[i].rotation, transforms[i]);
        }
        count = (int)Number;
    }

    public void Setup(List<GameObject> housePointList, ref List<StateController> stateControllers)
    {
       tmp = new List<StateController>(m_Instance.GetComponentsInChildren<StateController>());
        foreach (StateController stateController in tmp)
        {
            stateController.SetupAI(true, housePointList);
            stateControllers.Add(stateController);
        }
        EnsignFollow ensignFollow = Instantiate(ensign, stateControllers[0].transform.position + ensign.transform.position, ensign.transform.rotation).GetComponent<EnsignFollow>();
        ensignFollow.enemyLegion = this;
    }

    public StateController getFllowingStateController()
    {
        foreach (StateController stateController in tmp)
        {
            if (stateController.isActiveAndEnabled)
                return stateController;
        }
        return null;
    }
}
