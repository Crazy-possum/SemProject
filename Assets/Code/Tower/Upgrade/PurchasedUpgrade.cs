using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasedUpgrade
{
    private bool _isFirstUpgrade;
    private bool _isSecondUpgrade;
    private bool _isThirdUpgrade;

    public bool IsCannonFirstUpgrade { get => _isFirstUpgrade; set => _isFirstUpgrade = value; }
    public bool IsCannonSecondUpgrade { get => _isSecondUpgrade; set => _isSecondUpgrade = value; }
    public bool IsCannonThirdUpgrade { get => _isThirdUpgrade; set => _isThirdUpgrade = value; }
}
