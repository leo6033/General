﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Transform> transforms;
    public GameObject ensign;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int count;

    private LegionUtil[] legionUtils;
    private List<StateController> tmp;
    private string Types;
    

    public void Init(float Number, GameObject LegionPrefeb)
    {
        for (int i = 0; i < Number; i++)
        {
            Instantiate(LegionPrefeb, transforms[i].position, transforms[i].rotation, transform);
        }
        count = (int)Number;
    }

    public void Setup(ref List<StateController> stateControllers)
    {
        tmp = new List<StateController>(m_Instance.GetComponentsInChildren<StateController>());
        legionUtils = m_Instance.GetComponentsInChildren<LegionUtil>();
        foreach (StateController stateController in tmp)
        {
            stateController.SetupAI(true, null);
            stateController.targetPoint = stateController.transform.position + stateController.RelativePosition;
            stateControllers.Add(stateController);
        }
        EnsignFollow ensignFollow =  Instantiate(ensign, stateControllers[0].transform.position + ensign.transform.position, ensign.transform.rotation).GetComponent<EnsignFollow>();
        Types = tmp[0].Types;
        ensignFollow.playerLegion = this;
    }

    public StateController getFllowingStateController()
    {
        foreach(StateController stateController in tmp)
        {
            if (stateController.isActiveAndEnabled)
                return stateController;
        }
        return null;
    }

    public void OnSelect()
    {
        foreach (StateController stateController in tmp)
        {
            stateController.OnSelect();
        }
    }

    public void OnUnselect()
    {
        foreach (StateController stateController in tmp)
        {
            stateController.OnUnselect();
        }
    }

    public string getTypes()
    {
        return Types;
    }

    public int getCount()
    {
        return count;
    }
}
