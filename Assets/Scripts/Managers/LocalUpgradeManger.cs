using System.Collections.Generic;
using UnityEngine;

public class LocalUpgradeManager : UpgradeManager
{
    private void Awake(){
        UpdateStoreList();
    }
}
