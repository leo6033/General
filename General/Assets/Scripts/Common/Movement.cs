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
                Debug.Log(go.gameObject.name);
                if (go != null && go.tag == "Legion")
                {
                    PlaneCubeManager.showGrid();
                    m_Selected = true;
                    Time.timeScale = 0.1f;
                    go.GetComponent<PlayerManager>().OnSelect();
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && m_Selected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 11))
            {
                Transform m_TargetPoint = hit.transform;
                if (m_TargetPoint != null && m_TargetPoint.tag == "PlaneCube")
                {
                    Vector3 position = m_TargetPoint.position;
                    position.y +=  hit.collider.gameObject.GetComponent<PlaneCube>().height / 2;
                    Debug.Log(position);
                    StateController[] objs = go.GetComponentsInChildren<StateController>();
                    foreach (StateController stateController in objs)
                    {
                        stateController.targetPoint = position;
                        stateController.currentState = m_WalkState;
                        Debug.Log(transform.gameObject.name + " start" + m_WalkState);
                    }
                    go.GetComponent<PlayerManager>().OnUnselect();
                    m_Selected = false;
                    PlaneCubeManager.cancleGrid();
                    go = null;
                    Time.timeScale = 1f;
                }    
            }
        }
    }
}
