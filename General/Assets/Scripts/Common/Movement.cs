using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public State m_WalkState;

    private GameObject go;
    private bool m_Selected = false;
    private PlaneCubeManager PlaneCubeManager;

    private void Awake()
    {
        PlaneCubeManager = GetComponent<PlaneCubeManager>();   
    }

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
                {
                    PlaneCubeManager.showGrid();
                    m_Selected = true;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && m_Selected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 11))
            {
                Transform m_TargetPoint = hit.transform;
                if (m_TargetPoint != null && m_TargetPoint.tag == "PlaneCube")
                {
                    Debug.Log(m_TargetPoint.position);
                    StateController[] objs = go.GetComponentsInChildren<StateController>();
                    foreach (StateController stateController in objs)
                    {
                        stateController.targetPoint = m_TargetPoint;
                        stateController.currentState = m_WalkState;
                    }
                    m_Selected = false;
                    PlaneCubeManager.cancleGrid();
                    go = null;
                }    
            }
        }
    }
}
