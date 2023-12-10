using Firebase.Database;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private string userID;
    private DatabaseReference dbreference;

    private void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CoinUpdate()
    {
        dbreference.Child("users").Child(userID);
    }

}
