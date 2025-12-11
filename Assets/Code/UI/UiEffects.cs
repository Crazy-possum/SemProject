using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEffects : MonoBehaviour
{
    [SerializeField] private GameObject _enemyEnterVFX;
    [SerializeField] private Animator _enemyEnterAnimator;
    [SerializeField] private GameObject _lvlUpVFX;
    [SerializeField] private Animator _lvlUpAnimator;

    private void OnEnable()
    {
        EnemyMovement.OnEnemyEnter += EnemyEnterVFX;
    }

    private void EnemyEnterVFX()
    {
        //_enemyEnterVFX.SetActive(true);
       // _enemyEnterAnimator.Play("VFX_TowerUpgrade", -1, 0f);
    }
}
