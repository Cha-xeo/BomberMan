using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AplicationController
{
    public enum GameCondition
    {
        None,
        Won,
        Lost
    }


    public class AplicationController : MonoBehaviour
    {
        public static AplicationController Instance { get; private set; }
        public bool gameIsPaused;
        public bool isServer;
        [SerializeField] GameObject _canvasEndPanelRoots;
        [SerializeField] GameObject _winPanelRoots;
        [SerializeField] GameObject _losePanelRoots;


        public GameCondition gameCondition;
        void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            Application.wantsToQuit += OnWantToQuit;
            SceneManager.activeSceneChanged += OnSwitchScene;
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 120;
            gameCondition = GameCondition.None;
            SceneManager.LoadScene(Constants.TAG_OFFLINE_SCENE);

        }

        public void OnSwitchScene(Scene current, Scene next)
        {
            switch (next.name)
            {
                case Constants.TAG_OFFLINE_SCENE:
                    isServer = false;
                    switch (gameCondition)
                    {
                        case GameCondition.None:
                            _canvasEndPanelRoots.SetActive(false);
                            _winPanelRoots.SetActive(false);
                            _losePanelRoots.SetActive(false);
                            // do nothing
                            break;
                        case GameCondition.Won:
                            _canvasEndPanelRoots.SetActive(true);
                            _winPanelRoots.SetActive(true);
                            _losePanelRoots.SetActive(false);
                            // show Victoty
                            gameCondition = GameCondition.None;
                            break;
                        case GameCondition.Lost:
                            _canvasEndPanelRoots.SetActive(true);
                            _winPanelRoots.SetActive(false);
                            _losePanelRoots.SetActive(true);
                            // show defeat
                            gameCondition = GameCondition.None;
                            break;
                    }
                    break;
                case Constants.TAG_ONLINE_SCENE:
                    break;
                case Constants.TAG_GAMEPLAY_SCENE:
                    break;
                default: 
                    Debug.LogWarning("Scene not present in AplicationController");
                    break;
            }
        }

        public void HideEndScreen()
        {
            _canvasEndPanelRoots.SetActive(false);
        }

        
        public void quit()
        {
            StartCoroutine(LeaveBeforeQuit());
        }
        private bool OnWantToQuit()
        {
            //var canQuit = string.IsNullOrEmpty(m_LocalLobby?.LobbyID);
            bool canQuit = true;
            if (!canQuit)
            {
                StartCoroutine(LeaveBeforeQuit());
            }
            return true;
        }

        /// <summary>
        ///     In builds, if we are in a lobby and try to send a Leave request on application quit, it won't go through if we're quitting on the same frame.
        ///     So, we need to delay just briefly to let the request happen (though we don't need to wait for the result).
        /// </summary>
        private IEnumerator LeaveBeforeQuit()
        {
            Debug.Log("Quitting");
            yield return null;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
