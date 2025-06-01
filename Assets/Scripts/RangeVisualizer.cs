using UnityEngine;

public class RangeVisualizer : MonoBehaviour
{
    public Mesh circleMesh; // 원형 메쉬
    public Material rangeMaterial;
    public float range = 3f;
    public bool draw = false;

    void OnDrawGizmos()
    {
        if (draw == false) return;
        if (circleMesh && rangeMaterial)
        {
            Matrix4x4 matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one * range * 2f);
            rangeMaterial.color = new Color(0.1f, 0.1f, 1.0f, 0.5f);
            Graphics.DrawMesh(circleMesh, matrix, rangeMaterial, 3);
        }
    }
}