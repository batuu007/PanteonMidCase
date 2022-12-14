using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] public GridInfo gridInfo;
    [HideInInspector] public Vector3 offset;

    Vector3 spawnPoint;
    [SerializeField] Transform spawnFlag;
    [SerializeField] GameObject unitToSpawn;

    private void Start()
    {
        spawnPoint = spawnFlag.position;
    }

    private void Update()
    {
        if (SelectionController.Instance.selectedObject == transform)
        {
            spawnFlag.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(1))
            {
                bool iswalkable = GridSystem.Instance.NodeFromWorldPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).isWalkable;

                if (iswalkable)
                {
                    spawnFlag.position = GridSystem.Instance.NodeFromWorldPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).worldPosition;

                }

            }

        }
        else
            spawnFlag.gameObject.SetActive(false);
    }
    public void SpawnUnit()
    {
        if (unitToSpawn == null) return;
        GameObject unit = Instantiate(unitToSpawn, spawnPoint, Quaternion.identity);
        unit.GetComponent<Unit>().MoveTo(spawnFlag.position);
    }

}
