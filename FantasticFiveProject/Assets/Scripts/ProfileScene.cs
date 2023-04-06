using System; using System.Collections; using System.Collections.Generic;
using Firebase; using Firebase.Auth; using Firebase.Database;
using UnityEngine; using UnityEngine.UI;
using TMPro;

public class ProfileScene : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    public TMP_Text userInfo;
    public FirebaseAuth auth;
    public FirebaseDatabase datab;
    public FirebaseUser User;

    void Awake() {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                AddToInformation("Could not resolve all Firebase dependencies: " + dependencyStatus.ToString());
            }
        });
    }
    
    ///Connects to Firebase Authentication and Firebase Database
    private void InitializeFirebase()
    {
        AddToInformation("Setting up Firebase Authentication");

        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        //Set the database instance object
        datab = FirebaseDatabase.DefaultInstance;
    }

    private void AddToInformation(string str) { userInfo.text += "\n" + str; }

}
