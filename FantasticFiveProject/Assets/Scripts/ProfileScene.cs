using System; using System.Collections; using System.Collections.Generic; using System.Linq; using System.Threading.Tasks;
using Firebase; using Firebase.Auth; using Firebase.Database;
using UnityEngine; using UnityEngine.UI;
using TMPro;

using Google;

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
        AddToInformation("Setting up Firebase ");
        
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        
        AddToInformation("Welce: !");
    }


    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        
        AddToInformation("Welcome: " + task.Result.DisplayName + "!");
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    AddToInformation("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddToInformation("Canceled");
        }
        else
        {
            AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            AddToInformation("Email = " + task.Result.Email);
            //AddToInformation("Google ID Token = " + task.Result.IdToken);
            //AddToInformation("Email = " + task.Result.Email);
        }
    }


    private void AddToInformation(string str) { userInfo.text += "\n" + str; }

}
