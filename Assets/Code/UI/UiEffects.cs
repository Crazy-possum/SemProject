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
        ExperienceController.OnLevelUp += LevelUpVFX;
    }

    private void EnemyEnterVFX()
    {
        _enemyEnterVFX.SetActive(true);
        _enemyEnterAnimator.Play("VFX_enemyEnter", -1, 0f);
    }

    private void LevelUpVFX()
    {
        _lvlUpVFX.SetActive(true);
        _lvlUpAnimator.Play("VFX_lvlUp", -1, 0f);
    }
}
