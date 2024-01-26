
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundData", menuName = "ScriptableObjects/BackGroundSO")]

public class BackGroundSO : ScriptableObject
{
    [SerializeField] private List<BackGroundData> _listBackGround;
}
