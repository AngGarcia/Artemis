//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class GUIManager : TemporalSingleton<GUIManager>
//{
//    public Button     editModeButton;
//    public Button     gameModeButton;
//    public GameObject grid;
//    public Animator   platformList;

//    public GameObject platform;
//    public GameObject platform1;
//    public GameObject platform2;
//    public GameObject platform3;

//    public bool   canPick = false;
//    public bool   platformPicked = false;
//    public string platformID; 
//    public bool   followMouse = false;

//    private void Update()
//    {
//        if (canPick && Input.GetKeyDown(KeyCode.Mouse0))
//        {
//            followMouse = true;
//            platformPicked = true;

//            CreatePlatform(platformID);
//        }

//        if (Input.GetKeyUp(KeyCode.Mouse0) && platformPicked)
//        {
//            followMouse = false;
//            platformPicked = false;
            
//            PlacePlatform(true);

//            canPick = false;
//        }
//    }

//    public void ChangeMode(bool button)
//    {
//        editModeButton.gameObject.SetActive(button);
//        // GameManager.Instance.gameMode = button;
        
//        grid.SetActive(!button);
//        gameModeButton.gameObject.SetActive(!button);
//        platformList.gameObject.SetActive(!button);
//    }
    
//    public void OpenMenu(bool open)
//    {
//        if (open)
//        {
//            platformList.SetBool("open", true);
//            platformList.SetBool("close", false);
//        }
//        else
//        {
//            platformList.SetBool("open", false);
//            platformList.SetBool("close", true);
//        }
//    }

//    public void CreatePlatform(string platformID)
//    {
//        switch (platformID)
//        {
//            case "Platform1":
//                platform = Instantiate(platform1, Input.mousePosition, Quaternion.identity) as GameObject;
//                break;

//            case "Platform2":
//                platform = Instantiate(platform2, Input.mousePosition, Quaternion.identity) as GameObject;
//                break;

//            case "Platform3":
//                platform = Instantiate(platform3, Input.mousePosition, Quaternion.identity) as GameObject;
//                break;
//        }

//        PlacePlatform(false);
//    }

//    public void PlacePlatform(bool place)
//    {
//        if (platform.GetComponent<PlatformController>() != null)
//        {
//            platform.GetComponent<PlatformController>().placed = place;
//        }
//    }
//}
