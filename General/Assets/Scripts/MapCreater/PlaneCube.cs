using UnityEngine;

public class PlaneCube : MonoBehaviour
{
    //[HideInInspector]
    public bool isShowGrid = true;

    public float height;
    public Material planeCube;
    public Material planeCubeGrid;

    private bool isGrid = false;
    private void Awake()
    {
        height = transform.position.y + transform.localScale.y / 25 * 0.5f;
    }

    public void showGrid()
    {
        if (!isShowGrid) return;
        isGrid = true;
        Material[] materials = GetComponent<MeshRenderer>().materials;
        materials[1] = planeCubeGrid;
        GetComponent<Renderer>().materials = materials;
    }

    public void cancelGrid()
    {
        if (!isShowGrid) return;
        isGrid = false;
        Material[] materials = GetComponent<MeshRenderer>().materials;
        materials[1] = planeCube;
        GetComponent<Renderer>().materials = materials;
    }

    public void switchGrid()
    {
        if (!isShowGrid) return;
        if (isGrid)
        {
            cancelGrid();
        }
        else
        {
            showGrid();
        }
    }
}
