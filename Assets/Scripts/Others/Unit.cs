using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3[] _path;
    private int _targetIndex;

    private GridSystem _gridSystem;
    public bool isSelected;
    public GameObject sprite;
    private void Start()
    {
        _gridSystem = GameObject.FindGameObjectWithTag("Manager").GetComponent<GridSystem>();
    }
    private void Update()
    {
        if (SelectionController.Instance.selectedObject == transform)
        {
            sprite.SetActive(true);
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 targetPos = _gridSystem.NodeFromWorldPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).worldPosition;
                MoveTo(targetPos);
            }

        }
        else
            sprite.SetActive(false);

    }

    public void MoveTo(Vector3 targetPos)
    {
        PathRequestManager.RequestPath(transform.position, targetPos, OnPathFound);
    }

    void OnPathFound(Vector3[] newPath, bool success)
    {
        if (success)
        {
            _path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    IEnumerator FollowPath()
    {
        if (_path.Length > 0)
        {
            Vector3 currentWaypoint = _path[0];
            _targetIndex = 0;
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    _targetIndex++;
                    if (_targetIndex >= _path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = _path[_targetIndex];
                }
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, _speed * Time.deltaTime);
                yield return null;
            }
        }

    }
}
