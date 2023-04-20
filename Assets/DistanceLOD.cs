using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLOD : MonoBehaviour
{
    public Material Material;
    public Renderer[] OceanRenderers;
    public Mesh[] MeshLODs;
    private int[] m_rendererLODs;
    private List<Matrix4x4> m_closeRendererTRS;
    private List<Matrix4x4> m_midRendererTRS;
    private List<Matrix4x4> m_farRendererTRS;
    private Material[] m_lodMaterials;
    public float FarDistance = 150;
    public float MidDistance = 100;
    public float CloseDistance = 50;
    private Transform m_camera;
    private CullingGroup m_cullingGroup;
    private float[] m_distanceBands = new float[] { 30, 100, 150 };
    private bool m_frameReadyFlag;
    // Start is called before the first frame update
    void Start()
    {
        m_rendererLODs = new int[OceanRenderers.Length];
        m_camera = Camera.main.transform;
        m_closeRendererTRS = new List<Matrix4x4>();
        m_midRendererTRS = new List<Matrix4x4>();
        m_farRendererTRS = new List<Matrix4x4>();
        m_cullingGroup = new CullingGroup();
        m_cullingGroup.targetCamera = Camera.main;
        var gridBounds = new BoundingSphere[OceanRenderers.Length];
        for (int i = 0; i < gridBounds.Length; i++)
        {
            gridBounds[i] = new BoundingSphere(OceanRenderers[i].transform.position, 15f);
        }
        m_cullingGroup.SetBoundingSpheres(gridBounds);
        m_cullingGroup.SetDistanceReferencePoint(Camera.main.transform);
        m_cullingGroup.SetBoundingDistances(m_distanceBands);
        m_cullingGroup.onStateChanged = OnCullingStateChange;

        for (int i = 0; i < OceanRenderers.Length; i++)
        {
            OceanRenderers[i].gameObject.SetActive(false);
        }
        m_lodMaterials = new Material[MeshLODs.Length];
        for (int i = 0; i < m_lodMaterials.Length; i++)
        {
            m_lodMaterials[i] = Instantiate(Material);
        }
    }

    private void OnCullingStateChange(CullingGroupEvent cullingGroupEvent)
    {
        //prevent update result multiple times in a single frame
        if (!m_frameReadyFlag)
            return;

        m_frameReadyFlag = false;
        m_closeRendererTRS.Clear();
        m_midRendererTRS.Clear();
        m_farRendererTRS.Clear();
        for (int i = 0; i < OceanRenderers.Length; i++)
        {
            var grid = OceanRenderers[i].transform;
            var sqrD = (grid.position - m_camera.position).sqrMagnitude;

            if (sqrD <= CloseDistance * CloseDistance)
            {
                m_closeRendererTRS.Add(Matrix4x4.TRS(grid.position, grid.rotation, grid.localScale));
            }
            else if (sqrD <= MidDistance * MidDistance)
            {
                m_midRendererTRS.Add(Matrix4x4.TRS(grid.position, grid.rotation, grid.localScale));
            }
            else
            {
                m_farRendererTRS.Add(Matrix4x4.TRS(grid.position, grid.rotation, grid.localScale));
            }
        }
    }

    private void OnDestroy()
    {
        m_cullingGroup.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_closeRendererTRS.Count > 0)
        {
            Graphics.DrawMeshInstanced(MeshLODs[0], 0, Material, m_closeRendererTRS.ToArray(), m_closeRendererTRS.Count);
        }

        if (m_midRendererTRS.Count > 0)
        {
            Graphics.DrawMeshInstanced(MeshLODs[1], 0, Material, m_midRendererTRS.ToArray(), m_midRendererTRS.Count);
        }

        if (m_farRendererTRS.Count > 0)
        {
            Graphics.DrawMeshInstanced(MeshLODs[2], 0, Material, m_farRendererTRS.ToArray(), m_farRendererTRS.Count);
        }
        m_frameReadyFlag = true;
    }
}
