using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;

    void Start ()
    {
        turretToBuild = standardTurretPrefab;
    }

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

}
