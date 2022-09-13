using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBuilding : MonoBehaviour
{
    [SerializeField] private GameObject _objToProduce;

    private GridInfo _gridInfo;
    private Vector3 _offSet;
    private SpriteRenderer _spriteRenderer;
    private Color _firstColor, _secondColor;

    private void Start()
    {
        _gridInfo = _objToProduce.GetComponent<Building>().gridInfo;
        _offSet = _objToProduce.GetComponent<Building>().offset;
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _firstColor = new Color(0, 1, 0, 0.3f);
        _secondColor = new Color(1, 0, 0, 0.3f);
        _spriteRenderer.color = _firstColor;
        if (_gridInfo.height % 2 == 0)
            _offSet += Vector3.up * GridSystem.Instance.nodeRadius;
        if (_gridInfo.width % 2 == 0)
            _offSet += -Vector3.right * GridSystem.Instance.nodeRadius;
    }
    void Update()
    {
        transform.position = GridSystem.Instance.NodeFromWorldPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).worldPosition + _offSet;
        bool canMake = !Physics.CheckBox(transform.position, new Vector3(_gridInfo.width, _gridInfo.height) * GridSystem.Instance.nodeRadius, Quaternion.identity, GridSystem.Instance.unwalkableMask);
        if (canMake && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            _spriteRenderer.color = _firstColor;
            if (Input.GetMouseButtonDown(0) && GridManager.Instance.canBuild)
            {
                Instantiate(_objToProduce, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                GridManager.Instance.canBuild = false;
                //   GridSystem.Instance.CreateGrid();
                // for optimization
                GridSystem.Instance.UpdateGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offSet, _gridInfo.width, _gridInfo.height);
            }


        }
        else
            _spriteRenderer.color = _secondColor;
    }
}
