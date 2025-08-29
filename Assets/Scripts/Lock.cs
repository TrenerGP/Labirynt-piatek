using UnityEngine;

public class Lock : MonoBehaviour
{
    public DoorScript[] doors;
    public KeyColor myColor;
    bool canOpen = false;
    bool unlocked = false;
    Animator key;

    private void Start()
    {
        key = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canOpen = true;
            Debug.Log("You can open the lock");
            GameManager.gameManager.inGameText.text 
                = "Press E to open the lock";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canOpen = false;
            Debug.Log("You cannot open the lock");
            GameManager.gameManager.inGameText.text = "";
        }
    }

    public void UseKey()
    {
        foreach(var d in doors)
        {
            d.Open();
        }
    }

    public bool CheckKey()
    {
        if (GameManager.gameManager.redKey>0 && myColor==KeyColor.Red)
        {
            GameManager.gameManager.redKey--;
            GameManager.gameManager.redKeyText.text = GameManager.gameManager.redKey.ToString();
            unlocked = true;
            return true;
        }
        if (GameManager.gameManager.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.gameManager.greenKey--;
            GameManager.gameManager.greenKeyText.text = GameManager.gameManager.greenKey.ToString();
            unlocked = true;
            return true;
        }
        if (GameManager.gameManager.blueKey > 0 && myColor == KeyColor.Blue)
        {
            GameManager.gameManager.blueKey--;
            GameManager.gameManager.blueKeyText.text = GameManager.gameManager.blueKey.ToString();
            unlocked = true;
            return true;
        }
        Debug.Log("You don't have a key!");
        return false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canOpen && !unlocked)
        {
            key.SetBool("useKey", CheckKey());
        }
    }
}
