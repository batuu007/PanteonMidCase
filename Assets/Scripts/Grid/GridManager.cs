using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    public bool canBuild;

    public void ProduceBuild(GameObject previewBuilding)
    {
        if (!canBuild)
        {
            previewBuilding.SetActive(true);
            canBuild = true;
        }
    }
}
