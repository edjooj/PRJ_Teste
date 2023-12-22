using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Photon.Pun;
using System.Collections;
using UnityEngine;

    public class PlayerCustomizer : MonoBehaviourPunCallbacks
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
        public Customize customize;

    private void Start()
    {
        if (!photonView.IsMine) { return; }
        customize = FindObjectOfType<Customize>();

        StartCoroutine(LoadCustomizePlayerCoroutine());
    }

    public void SaveRoupa() //FUNÇÃO PARA MANDAR INFORMAÇÕES NO FIREBASE
    {
        Debug.Log("SaveRoupa: Chamando SaveCustomizePlayer()");
        SaveCustomizePlayer();
    }

    public override void OnJoinedRoom() //AO ENTRAR NO PHOTON, CARREGAR DO FIREBASE
    {
        StartCoroutine(LoadCustomizePlayerCoroutine());
    }

    public void AtualizarRoupa()
    {
        StartCoroutine (LoadCustomizePlayerCoroutine());
    }

    public IEnumerator LoadCustomizePlayerCoroutine() //PEGAR INFORMAÇÕES DO FIREBASE
    {
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;

        string userId = FirebaseCORE.instance.authManager.user.UserId;

        DatabaseReference playerClothsRef = DBreference.Child("users").Child(userId).Child("PlayerClouths");

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

        if (camisaTask.IsCompleted && cabeloTask.IsCompleted && calcaTask.IsCompleted && chapeuTask.IsCompleted && sapatoTask.IsCompleted)
        {
            // Converte os resultados das tarefas para inteiros e os atribui às variáveis.
            camisa = int.Parse(camisaTask.Result.Value.ToString());
            cabelo = int.Parse(cabeloTask.Result.Value.ToString());
            calca = int.Parse(calcaTask.Result.Value.ToString());
            chapeu = int.Parse(chapeuTask.Result.Value.ToString());
            sapato = int.Parse(sapatoTask.Result.Value.ToString());

            customize.MeshSelect();

            photonView.RPC("UpdatePlayerCustomization", RpcTarget.AllBuffered, camisa, cabelo, calca, chapeu, sapato);
        }
    }

    [PunRPC]
    public void UpdatePlayerCustomization(int camisa, int cabelo, int calca, int chapeu, int sapato)
    {
        this.camisa = camisa;
        this.cabelo = cabelo;
        this.calca = calca;
        this.chapeu = chapeu;
        this.sapato = sapato;

        customize.MeshSelect();
    }


    public void SaveCustomizePlayer() // SALVAR NO FIREBASE AS INFORMAÇÕES CONFORME APERTA BOTÕES
    {

        Debug.Log("SaveCustomizePlayer: Tentando salvar customização no Firebase.");
        if (!string.IsNullOrEmpty(FirebaseCORE.instance.authManager.user.UserId))
        {
            Debug.Log($"SaveCustomizePlayer: UserId é {FirebaseCORE.instance.authManager.user.UserId}");

            string userId = FirebaseCORE.instance.authManager.user.UserId;
            DatabaseReference playerClothsRef = FirebaseDatabase.DefaultInstance.RootReference
                .Child("users")
                .Child(userId)
                .Child("PlayerClouths");

            Debug.Log($"SaveCustomizePlayer: Salvando camisa {NetworkController.instance.customize.camisa}");


            playerClothsRef.Child("camisa").SetValueAsync(NetworkController.instance.customize.camisa)
            .ContinueWith(task => {
                if (task.IsFaulted)
                {
                    // Houve um erro ao salvar no Firebase, log o erro
                    Debug.LogError("SaveCustomizePlayer: Erro ao salvar 'camisa' no Firebase.");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("SaveCustomizePlayer: 'camisa' salva com sucesso no Firebase.");
                }
            });
            playerClothsRef.Child("cabelo").SetValueAsync(NetworkController.instance.customize.cabelo);
            playerClothsRef.Child("calca").SetValueAsync(NetworkController.instance.customize.calca);
            playerClothsRef.Child("chapeu").SetValueAsync(NetworkController.instance.customize.chapeu);
            playerClothsRef.Child("sapato").SetValueAsync(NetworkController.instance.customize.sapato);
        }
        else
        {
            Debug.LogError("SaveCustomizePlayer: UserId é nulo ou vazio.");
        }
    }

    public void OnSomeButtonClicked()
    {
        Debug.Log("OnSomeButtonClicked: Botão pressionado, chamando SaveRoupa()");
        SaveRoupa();
    }


}
