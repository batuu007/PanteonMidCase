using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionController : Singleton<SelectionController>
{
    [SerializeField] private LayerMask _mask;
    public Transform selectedObject;
    [SerializeField] public UnityEvent OnBuildSelected;
    [SerializeField] public UnityEvent OnNotBuildSelected;
    [SerializeField] public Image buildingImage;
    [SerializeField] public Image productionImage;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, _mask);
            if (hasHit)
            {
                selectedObject = hit.transform;
                if (selectedObject.CompareTag("Building"))
                {
                    OnBuildSelected?.Invoke();

                }
                else if (selectedObject.CompareTag("Unit"))
                {
                    OnNotBuildSelected?.Invoke();
                }
            }
            else
            {

                selectedObject = null;
                OnNotBuildSelected?.Invoke();
            }

        }
    }
    public void UIUpdate()
    {
        if (selectedObject.GetComponent<Building>() == null) return;

        Building b = selectedObject.GetComponent<Building>();
        buildingImage.sprite = b.gridInfo.image;
        productionImage.sprite = b.gridInfo.productionImage;
    }
    public void Spawn()
    {
        if (selectedObject.GetComponent<Building>() == null) return;

        Building b = selectedObject.GetComponent<Building>();
        b.SpawnUnit();
    }
}

