using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }


    public void PurchaseStandardTurret ()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseSecondTurret()
    {
        Debug.Log("Second Turret Selected");
        buildManager.SetTurretToBuild(buildManager.secondTurretPrefab);
    }
}
