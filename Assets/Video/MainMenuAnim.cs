using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainMenuAnim : MonoBehaviour
{
    private Animator _menuAnimator;

    private void Start()
    {
        _menuAnimator = GetComponent<Animator>();
    }

    private void ChangeAnim()
    {
        _menuAnimator.SetBool("Loop", true);
    }
}
