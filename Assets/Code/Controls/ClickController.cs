using UnityEngine;

public class ClickController : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private TowerBuilder _towerBuilder;
    [Tooltip("Панель с выбором башни на постройку")]
    [SerializeField] private GameObject _towerBuildPanel;
    [SerializeField] private GameObject _towerButton;

    private ScriptableListScript _towerObjectListSO;
    private bool _isTownButtenHere;

    private void Start()
    {
        _towerObjectListSO = Resources.Load<ScriptableListScript>("Tower/TowerObjects_SO");
    }

    public void ClickBehavior(GameObject objectUnderMouse)
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (objectUnderMouse.TryGetComponent(out BuildPointTag towerBehavior))
            {
                InitializeButton();
                _towerBuilder.BuildPointObject = objectUnderMouse;
                _towerBuildPanel.SetActive(true);
            }
        }
    }

    private void InitializeButton()
    {
        if (!_isTownButtenHere)
        {
            int buttonAmount = 4;

            for (int i = 0; i < buttonAmount; i++)
            {
                GameObject uiGObject = GameObject.Instantiate(_towerButton, _towerBuildPanel.transform);
                DecisionButton buttonScript = uiGObject.GetComponentInChildren<DecisionButton>();

                switch (i)
                {
                    case 0: buttonScript.LocalTowerEnum = TowerEnum.Cannon; break;
                    case 1: buttonScript.LocalTowerEnum = TowerEnum.Shotgun; break;
                    case 2: buttonScript.LocalTowerEnum = TowerEnum.Catapult; break;
                    case 3: buttonScript.LocalTowerEnum = TowerEnum.Sniper; break;
                }

                TowerScriptable towerSO = _towerObjectListSO.SOList.Find(item => item.TowerEnum == buttonScript.LocalTowerEnum);
                buttonScript.TowerBuilder = _towerBuilder;
                buttonScript.TowerUpgrader = _towerBuilder.GetComponentInParent<TowerUpgrader>();
                buttonScript.TowerBuildPanel = _towerBuildPanel;
                buttonScript.CustomizationButton(towerSO);
            }
            _isTownButtenHere = true;
        }
    }
}
