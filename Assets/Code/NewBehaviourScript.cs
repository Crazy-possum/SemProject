using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private ExperienceController ExperienceController;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(AddExp);
    }

    private void AddExp()
    {
        Debug.Log(ExperienceController.CurrentExp);
        ExperienceController.CurrentExp += 10;
    }
}
