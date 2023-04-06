using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Google;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoogleSignInDemo : MonoBehaviour
{
    //public static GoogleSignInDemo instance;

    public TMP_Text infoText;
    //TMP_Text infoText;
    public string webClientId = "<your client id here>";

    private FirebaseAuth auth;
    private FirebaseUser User;
    private FirebaseDatabase datab;
    public DependencyStatus dependencyStatus;
    private GoogleSignInConfiguration configuration;

    /*private void Awake()
    {
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        
        CheckFirebaseDatabase();
        CheckFirebaseAuthentication();
    }

    private void CheckFirebaseAuthentication()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {   
                    //datab = FirebaseDatabase.DefaultInstance;
                    auth = FirebaseAuth.DefaultInstance;
                    }
                else
                {AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());}
            }
            else
            {
                AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }

    private void CheckFirebaseDatabase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {   
                    datab = FirebaseDatabase.DefaultInstance;
                    //auth = FirebaseAuth.DefaultInstance;
                    }
                else
                {AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());}
            }
            else
            {
                AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }*/

    async void Awake() 
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            // all good, firebase init passed
            AddToInformation("Firebase Loaded");
            InitializeFirebase();
        }
        else
        {
            AddToInformation("Firebase Failed");
        }
    }

    private void InitializeFirebase()
    {
        AddToInformation("Setting up Firebase Authentication");

        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        //Set the database instance object
        datab = FirebaseDatabase.DefaultInstance;
        AddToInformation("Setting up Firebase ");
        AddToInformation("Welce: !");
    }


    public void SignInWithGoogle() { OnSignIn(); }
    public void SignOutFromGoogle() { OnSignOut(); }

    private void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void OnSignOut()
    {
        AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        AddToInformation("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
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
            //AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            //AddToInformation("Email = " + task.Result.Email);
            //AddToInformation("Google ID Token = " + task.Result.IdToken);
            //AddToInformation("Email = " + task.Result.Email);
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
            }
            else
            {
                AddToInformation("1. Sign In Successful.");
                User = task.Result;
                AddToInformation("2. Signed In and user made");
                StartCoroutine(DatabaseRegistration(User));
                AddToInformation("5. Signed In, user made");
            }
        });

        AddToInformation("6. Sign In Successful and user made" );
    }

    private IEnumerator DatabaseRegistration(Firebase.Auth.FirebaseUser _user)
    {        
        AddToInformation("3. Sign In Successful and user made" + User.UserId.ToString());   
        //AddToInformation("3. Sign In Successful and user made" + datab.ToString());   
        var name = datab.RootReference.Child("users").Child(User.UserId).Child("name").SetValueAsync(User.DisplayName);
        
        AddToInformation("Sign In Successful and user made" + User.Email.ToString());
        var email = datab.RootReference.Child("users").Child(User.UserId).Child("email").SetValueAsync(User.Email);
        
        //var name = datab.RootReference.Child("users").Child(User.UserId).Child("name").SetValueAsync(User.DisplayName);
        yield return new WaitUntil(predicate: () => name.IsCompleted);    
        
        AddToInformation("4.Sign In Successful and user made" + User.DisplayName.ToString());
    }



    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        AddToInformation("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void AddToInformation(string str) { infoText.text += "\n" + str; }

}