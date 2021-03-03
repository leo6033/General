using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private GameObject go;
    private bool m_Selected = false;
    public Transform m_TargetPoint;

    public State m_WalkState;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !m_Selected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
            {
                go = hit.transform.parent.gameObject;    //获得选中物体
                if (go != null)
                    m_Selected = true;
            }
        }
        else if (Input.GetMouseButtonDown(0) && m_Selected)
        {
            StateController[] objs = go.GetComponentsInChildren<StateController>();
            foreach(StateController stateController in objs)
            {
                stateController.targetPoint = m_TargetPoint;
                stateController.currentState = m_WalkState;
            }
            m_Selected = false;
            go = null;
        }
    }
}
