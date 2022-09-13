using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    private Queue<PathRequest> _pathRequests = new Queue<PathRequest>();
    private PathRequest _currentRequest;

    private static PathRequestManager instance;

    private PathFinding _pathFinding;

    private bool _isProcessing;

    private void Awake()
    {
        instance = this;
        _pathFinding = GetComponent<PathFinding>();
    }
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance._pathRequests.Enqueue(newRequest);
        instance.TryProcessNext();

    }

    private void TryProcessNext()
    {
        if (!_isProcessing && _pathRequests.Count > 0)
        {
            _currentRequest = _pathRequests.Dequeue();
            _isProcessing = true;
            _pathFinding.StartFindPath(_currentRequest.pathStart, _currentRequest.pathEnd);
        }
    }
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {

        _currentRequest.callback(path, success);
        _isProcessing = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}
