using UnityEngine;
using System.Collections;

public class SavedState : MonoBehaviour {
    private PlayerController[] controllers = new PlayerController[4];
    private InputManager[] managers = new InputManager[]
    {
        new InputManager(1),
        new InputManager(2),
        new InputManager(3),
        new InputManager(4)
    };
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        foreach (InputManager i in managers)
        {
            i.OnStart += PlayerEnter;
        }
	}
	
	// Update is called once per frame
	void Update () {
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

    private void PlayerEnter(InputManager manager)
    {
        if (manager.Player != null)
            return;
        foreach (PlayerController player in controllers)
        {
            if (player != null && player.Input == null)
            {
                player.Input = manager;
                return;
            }
        }
    }
}
