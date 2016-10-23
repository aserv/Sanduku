using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SavedState : MonoBehaviour {
    private float created;
    private PlayerController[] controllers = new PlayerController[4];
    private InputManager[] managers = new InputManager[]
    {
        new InputManager(1),
        new InputManager(2),
        new InputManager(3),
        new InputManager(4)
    };
    private bool init;
    [SerializeField]
    private int playerSpawned;
    [SerializeField]
    private int playersAlive;
	// Use this for initialization
	void Start () {
        created = Time.time;
        SavedState[] saved = GameObject.FindObjectsOfType<SavedState>();
        foreach (SavedState s in saved)
        {
            if (s == this)
                continue;
            if (s.created < this.created)
                Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        foreach (InputManager i in managers)
        {
            i.OnStart -= PlayerEnter; //Try remove old handler
            i.OnStart += PlayerEnter;
        }
        init = false;
    }

	
	// Update is called once per frame
	void Update () {
        if (!init)
        {
            playerSpawned = 0;
            playersAlive = 0;
            for (int c = 0; c < 4; c++)
            {
                if (managers[c].PlayerId != 0)
                {
                    playerSpawned++;
                    playersAlive++;
                    if (controllers[managers[c].PlayerId - 1] == null)
                        return;
                    controllers[managers[c].PlayerId-1].Input = managers[c];
                }
            }
            init = true;
        }
        InputManager.Waiting = playerSpawned < 2;
	    foreach (InputManager  i in managers)
        {
            if (i.Player == null)
                i.Update();
        }
	}

    public void InitPlayerControler(PlayerController player)
    {
        controllers[player.PlayerID-1] = player;
        player.gameObject.SetActive(false);
    }

    public void DeinitPlayerControler(PlayerController player)
    {
        playersAlive--;
        for (int c = 0; c < 4; c++)
        {
            if (controllers[c] == player)
                controllers[c] = null;
        }
        if (playersAlive == 1)
        {
            Input.ResetInputAxes();
            init = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void PlayerEnter(InputManager manager)
    {
        if (manager.Player != null)
            return;
        foreach (PlayerController player in controllers)
        {
            if (player != null && player.Input == null)
            {
                playerSpawned++;
                playersAlive++;
                manager.PlayerId = player.PlayerID;
                player.Input = manager;
                return;
            }
        }
    }
}
