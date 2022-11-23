using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class SkillBoxManager : MonoBehaviour
{
    [SerializeField] Skillbox[] skillboxes;
    public void SendJSON()
    {
        List<Skill> skillList = new List<Skill>();
        foreach (var item in skillboxes) skillList.Add(item.ReturnClass());
    }
}
