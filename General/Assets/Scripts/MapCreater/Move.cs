using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    public GameObject go;
    public GameObject go2;
    private NavMeshAgent m_navMeshAgent;  //寻路组件
    public Transform m_tPlayer;  //Player
    
    void Awake()
    {
        go.AddComponent<NavMeshSurface>();
        go.GetComponent<NavMeshSurface>().BuildNavMesh();
        //go2.AddComponent<NavMeshSurface>();
        //go2.GetComponent<NavMeshSurface>().BuildNavMesh();
        m_navMeshAgent = this.GetComponent<NavMeshAgent>();
        m_navMeshAgent.SetDestination(m_tPlayer.position);  //设定寻路目的地为Player所在地
    }
    void Update() {
        //m_navMeshAgent.Move((m_tPlayer.position - transform.position) * Time.deltaTime);
        
    }
}