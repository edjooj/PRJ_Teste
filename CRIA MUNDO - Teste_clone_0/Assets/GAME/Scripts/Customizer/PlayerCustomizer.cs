    using Firebase;
    using Firebase.Auth;
    using Firebase.Database;
using System.Collections;
using UnityEngine;

    public class PlayerCustomizer : MonoBehaviour
    {
        public int camisa;
        public int cabelo;
        public int calca;
        public int chapeu;
        public int sapato;

        // Variaveis Firebase
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser user;

        public DatabaseReference DBreference;

    private void Start()
    {
         StartCoroutine(LoadCustomizePlayerCoroutine());
    }

    public void AtualizarRoupa()
    {
        Customize.instance.MeshSelect();
        SaveCustomizePlayer();
    }

    public IEnumerator LoadCustomizePlayerCoroutine()
    {
        Debug.Log("Iniciando LoadCustomizePlayer");
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;

        string userId = FirebaseCORE.instance.authManager.user.UserId;
        Debug.Log("UserId: " + userId);

        DatabaseReference playerClothsRef = DBreference.Child("users").Child(userId).Child("PlayerClouths");

        // Espera até que todas as peças da roupa sejam carregadas.
        var camisaTask = playerClothsRef.Child("camisa").GetValueAsync();
        yield return new WaitUntil(() => camisaTask.IsCompleted);
        var cabeloTask = playerClothsRef.Child("cabelo").GetValueAsync();
        yield return new WaitUntil(() => cabeloTask.IsCompleted);
        var calcaTask = playerClothsRef.Child("calca").GetValueAsync();
        yield return new WaitUntil(() => calcaTask.IsCompleted);
        var chapeuTask = playerClothsRef.Child("chapeu").GetValueAsync();
        yield return new WaitUntil(() => chapeuTask.IsCompleted);
        var sapatoTask = playerClothsRef.Child("sapato").GetValueAsync();
        yield return new WaitUntil(() => sapatoTask.IsCompleted);

        // Verifica se houve algum erro nas tarefas.
        if (camisaTask.IsFaulted || cabeloTask.IsFaulted || calcaTask.IsFaulted || chapeuTask.IsFaulted || sapatoTask.IsFaulted)
        {
            Debug.LogError("Erro ao carregar customização de roupa.");
        }
        else
        {
            // Aplica as customizações carregadas.
            NetworkController.instance.customize.camisa = int.Parse(camisaTask.Result.Value.ToString());
            NetworkController.instance.customize.cabelo = int.Parse(cabeloTask.Result.Value.ToString());
            NetworkController.instance.customize.calca = int.Parse(calcaTask.Result.Value.ToString());
            NetworkController.instance.customize.chapeu = int.Parse(chapeuTask.Result.Value.ToString());
            NetworkController.instance.customize.sapato = int.Parse(sapatoTask.Result.Value.ToString());

            // Atualiza o visual do personagem.
            StartCoroutine(ApplyMeshCustomization());
        }
    }

    private IEnumerator ApplyMeshCustomization()
    {

        Customize.instance.MeshSelect();

        yield return null;
    }

    public void SaveCustomizePlayer()
    {
        if (FirebaseCORE.instance.authManager.user.UserId != null)
        {
            string userId = FirebaseCORE.instance.authManager.user.UserId;
            DatabaseReference playerClothsRef = FirebaseDatabase.DefaultInstance.RootReference
                .Child("users")
                .Child(userId)
                .Child("PlayerClouths");

            // Agora, em vez de salvar um objeto completo, você salva cada item individualmente.
            playerClothsRef.Child("camisa").SetValueAsync(NetworkController.instance.customize.camisa);
            playerClothsRef.Child("cabelo").SetValueAsync(NetworkController.instance.customize.cabelo);
            playerClothsRef.Child("calca").SetValueAsync(NetworkController.instance.customize.calca);
            playerClothsRef.Child("chapeu").SetValueAsync(NetworkController.instance.customize.chapeu);
            playerClothsRef.Child("sapato").SetValueAsync(NetworkController.instance.customize.sapato);
        }
        else
        {
            Debug.LogWarning("User not authenticated. Cannot update player customization.");
        }
    }


}
