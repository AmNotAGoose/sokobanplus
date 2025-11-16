using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public GameObject persistentObject;

    private void Start()
    {
        DontDestroyOnLoad(persistentObject);
    }
}
