using UnityEngine;
using System.Collections.Generic;

public class GemSpawner : MonoBehaviour
{
    [Header("khu vuc spawn")]
    [SerializeField] private Vector3 areaCenter = Vector3.zero;
    [SerializeField] private Vector3 areaSize = new Vector3(50f, 0f, 168f);

    [Header("raycast do be mat")]
    [SerializeField] private float RaycastHeightAbove = 50f;
    [SerializeField] private float RaycastMaxDistance = 100f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private int maxAttempsPerSpawn = 30;

    [Header("kiem tra vat can tai diem spawn")]
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float obstacleCheckRadius = 0.3f;
    [SerializeField] private float spawnHeightOffset = 0.3f;

    [Header("gioi han do doc be mat")]
    [SerializeField] private float maxSurfaceAngle = 60f;
    [SerializeField] private float minSurfaceAngle = -60f;

    [Header("thoi gian")]
    [SerializeField] private float spawnInterval = 1f;

    [Header("gioi han so gem ton tai cung luc")]
    [SerializeField] private int maxGemAlive = 15;

    [Header("Cac loai gem co the spawn")]
    [SerializeField] private GameObject[] gemPrefabs;
    [SerializeField] private List<GameObject> activeGems = new List<GameObject>();
    private float timer;

    private void Update()
    {
        activeGems.RemoveAll(g => g == null || !g.activeInHierarchy);

        timer += Time.deltaTime;

        if(timer >= spawnInterval && activeGems.Count < maxGemAlive){
            timer = 0f;
            TrySpawnGem();
        }
    }

    public void TrySpawnGem()
    {
        if (TryGetRandomValidPosition(out Vector3 spawnPos))
        {
            GameObject chosenPrefab = gemPrefabs[Random.Range(0, gemPrefabs.Length)];
            GameObject gem = GemPool.Instance.GetGem(chosenPrefab, spawnPos, Quaternion.identity);
            activeGems.Add(gem);
        }
        else
        {
            Debug.LogWarning("GemSpawner: Không tìm được vị trí spawn hợp lệ sau nhiều lần thử.");
        }
    }

    private bool TryGetRandomValidPosition(out Vector3 result)
    {
        for (int i = 0; i < maxAttempsPerSpawn; i++)
        {
            float x = areaCenter.x + Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float z = areaCenter.z + Random.Range(-areaSize.z / 2f, areaSize.z / 2f);
            Vector3 rayStart = new Vector3(x, areaCenter.y + RaycastHeightAbove, z);

            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, RaycastMaxDistance, groundMask))
            {
                float angle = Vector3.Angle(hit.normal, Vector3.up);
                if (angle > maxSurfaceAngle || angle < minSurfaceAngle)
                    continue;

                Vector3 checkPos = hit.point + Vector3.up * spawnHeightOffset;

                if (Physics.CheckSphere(checkPos, obstacleCheckRadius, obstacleMask))
                    continue;

                result = checkPos;
                return true;
            }
        }
        result = areaCenter;
        return false;
    }

}
