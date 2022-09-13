using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Building Info", menuName = "Custom/Building")]
public class GridInfo : ScriptableObject
{
    public int height, width;
    public Sprite image;
    public Sprite productionImage;
}
