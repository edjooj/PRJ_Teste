using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;

public class FirebaseAuthManager : MonoBehaviour
{
    // Variaveis Firebase
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;

    // Variaveis Login
    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    // Variaveis Registro
    [Space]
    [Header("Registration")]
    public TMP_InputField nameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;

    // Variaveis Registro
    [Space]
    [Header("Photon")]
    public ConnectionPhoton photon;

    private void Awake()
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
                Debug.Log("Desconectado: " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Logado cmo: " + user.UserId);
            }
        }
    }

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

            photon.SetUserName(user.DisplayName);
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterAsync(nameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword)
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

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;


                    string failedMessage = "Atualização de perfil falhou, devido:  ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "Email é invalido";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "senha está errada";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "Email não encontrado";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Password não encontrado";
                            break;
                        default:
                            failedMessage = "Atualização de perfil falhou";
                            break;
                    }

                    Debug.Log(failedMessage);
                }
                else
                {
                    Debug.Log("Registradoi com sucesso como: " + user.DisplayName);
                    UIManager.Instance.OpenLoginPanel();
                }
            }
        }
    }
}
