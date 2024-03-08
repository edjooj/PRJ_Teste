using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;

public class AuthenticationFirebase : MonoBehaviour
{
    // Variaveis Firebase
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DBreference;
    public TMP_Text uid;

    public bool firstTime = true;

    // Variaveis Login
    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    public TMP_Text loginTxt;
    public Button loginBtbn;
    public GameObject inicioPanel;
    public GameObject loginPanel;

    // Variaveis Registro
    [Space]
    [Header("Registration")]
    public TMP_InputField userRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;
    public TMP_InputField aniverRegisterField;
    public TMP_Text sexoRegisterField;

    // Variaveis Registro
    [Space]
    [Header("Photon")]
    public ConnectionPhoton photon;



    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Não foi possivel carregar as dependencias do Firebase " + dependencyStatus);
            }
        });
    }

    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
        loginTxt.text = "LOGIN";
        loginBtbn.onClick.AddListener(LoginButton);

        if (user != null) // Auto login
        {

            uid.text = user.UserId.ToString();
            Debug.Log("Usuário logado" + user.DisplayName);
            loginTxt.text = "ENTRAR";
            loginBtbn.onClick.AddListener(EntrarButton);
        }
    }

    #region ButtonChange
    void LoginButton()
    {
        loginPanel.SetActive(true);
        inicioPanel.SetActive(false);
        
    }

    void EntrarButton()
    {
        Debug.LogFormat("{0} Voce se logou com sucesso como:", user.DisplayName);
        CORE.instance.status.userName = user.DisplayName;
        CORE.instance.connection.ConnectToMaster();

        UpdatePlayerNickInFirebase();

        photon.SetUserName(user.DisplayName);
    }

    #endregion

    #region Login 
    public void Login()
    {
        StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;


            string failedMessage = "Login falhou, devido: ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "Email é invalido";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Senha está errada";
                    break;
                case AuthError.MissingEmail:
                    failedMessage += "Email está errado";
                    break;
                case AuthError.MissingPassword:
                    failedMessage += "Senhaa não encontrada";
                    break;
                default:
                    failedMessage = "Login falhou";
                    break;
            }

            Debug.Log(failedMessage);
        }
        else // Se o login for bem sucedido
        {
            user = loginTask.Result.User;

            Debug.LogFormat("{0} Voce se logou com sucesso como:", user.DisplayName);
            CORE.instance.status.userName = user.DisplayName;
            CORE.instance.connection.ConnectToMaster();

            UpdatePlayerNickInFirebase();

            photon.SetUserName(user.DisplayName);
        }
    }

    #endregion

    #region Convidado(Anonimo)
    public async void Convidado()
    {
        try
        {
            AuthResult result = await auth.SignInAnonymouslyAsync();
            user = result.User;

            Debug.LogFormat("User signed in anonymously: {0}", user.UserId);

            await CheckFirstTimeLogin(user);

            if (firstTime)
            {
                await FirstTimeNameGenerator(user);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"SignInAnonymouslyAsync encountered an error: {ex}");
        }
    }




    private async Task FirstTimeNameGenerator(FirebaseUser user)
    {
        await Task.Delay(10);
        string randomUsername = "CRIA" + UnityEngine.Random.Range(0, 10000).ToString("0000");

        Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
        {
            DisplayName = randomUsername,
        };

        try
        {
            await user.UpdateUserProfileAsync(profile);
            Debug.Log("User profile updated successfully." + randomUsername);
        }
        catch (Exception ex)
        {
            Debug.LogError($"UpdateUserProfileAsync encountered an error: {ex}");
        }
    }

    private async Task CheckFirstTimeLogin(FirebaseUser user)
    {
        var userRef = DBreference.Child("users").Child(user.UserId);

        var dataSnapshot = await userRef.GetValueAsync();
        if (dataSnapshot.Exists) //Usuário existe
        {
            firstTime = false;

            Debug.LogFormat("{0} Voce se logou com sucesso como:", user.DisplayName);
            CORE.instance.status.userName = user.DisplayName;
            CORE.instance.connection.ConnectToMaster();
        }
        else //Usuário não existe
        {
            firstTime = true;
            var userEntry = new Dictionary<string, object>
        {
            { "firstTime", false },
            { "PlayerNick", user.DisplayName },
        };
            await userRef.UpdateChildrenAsync(userEntry);
        }
    }
    #endregion

    #region Registro

    public void Register()
    {
        StartCoroutine(RegisterAsync(userRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text, aniverRegisterField.text, sexoRegisterField.text));
    }

    private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword, string aniver, string sexo)
    {
        if (name == "")
        {
            Debug.LogError("Usuário está vazio");
        }
        else if (email == "")
        {
            Debug.LogError("Email está vazio");
        }
        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
        {
            Debug.LogError("Ãs senhas não combinam");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);

                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "O registro falhou devido:  ";
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "Email invalido";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Senha errada";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Email não encontrado";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Senha não encontrada";
                        break;
                    default:
                        failedMessage = "Registro falho";
                        break;
                }

                Debug.Log(failedMessage);
            }
            else
            {
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = name };

                var profileTask = user.UpdateUserProfileAsync(userProfile);
                yield return new WaitUntil(() => profileTask.IsCompleted);

                if (profileTask.Exception != null)
                {
                    Debug.LogError(profileTask.Exception);
                }
                else
                {
                    Debug.LogFormat("Usuário criado com sucesso: {0} ({1})", user.DisplayName, user.Email);
                    CORE.instance.status.userName = user.DisplayName;
                    CORE.instance.connection.ConnectToMaster();
                    UpdatePlayerNickInFirebase();
                    photon.SetUserName(user.DisplayName);
                }
            }
        }
    }

    private void UpdatePlayerNickInFirebase()
    {
        var userRef = DBreference.Child("users").Child(user.UserId);

        var userEntry = new Dictionary<string, object>
        {
            { "PlayerNick", user.DisplayName },
            { "Aniversário", aniverRegisterField.text },
            { "Sexo", sexoRegisterField.text}
        };

        userRef.UpdateChildrenAsync(userEntry);
    }


    #endregion

}
