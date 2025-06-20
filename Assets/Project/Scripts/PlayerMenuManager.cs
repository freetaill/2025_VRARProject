using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerMenuManager : MonoBehaviour
{
    public GameObject player;
    public Transform head;

    public GameObject L_controller;
    public GameObject R_comtroller;
    public GameObject menu;
    public InputActionProperty showButton;

    public GameObject magicMenu;
    public List<Image> magic;
    public GameObject[] magicSpown;
    int magicNum;
    public InputActionProperty magicShowButton;

    float zAngle;

    bool hasMagic = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        magic[0] = magicMenu.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>();
        magic[1] = magicMenu.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Image>();
        magic[2] = magicMenu.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Image>();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }

        if (magicShowButton.action.WasPressedThisFrame() && !hasMagic)
        {
            magicMenu.SetActive(true);
            Debug.Log("마법");
        }

        if (menu.activeSelf)
        {
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * 1.5f;
        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
        if (magicMenu.activeSelf)
        {
            zAngle = L_controller.transform.localRotation.eulerAngles.z;
            if (zAngle > 180f) zAngle -= 360f;

            magicMenu.transform.position = L_controller.transform.position + new Vector3(L_controller.transform.forward.x/2, 0.0f, L_controller.transform.forward.z/2);
            magicMenu.transform.LookAt(new Vector3(L_controller.transform.position.x, magicMenu.transform.position.y, L_controller.transform.position.z));
            magicMenu.transform.forward *= -1;

            Debug.Log(L_controller.transform.localRotation.z);

            if (zAngle > 35f)
            {
                magic[0].color = Color.red;
                magic[1].color = Color.white;
                magic[2].color = Color.white;
                magicNum = 0;
            }
            else if (zAngle < -35f)
            {
                magic[0].color = Color.white;
                magic[1].color = Color.white;
                magic[2].color = Color.red;
                magicNum = 2;
            }
            else
            {
                magic[0].color = Color.white;
                magic[1].color = Color.cyan;
                magic[2].color = Color.white;
                magicNum = 1;
            }
        }
        
        if (magicShowButton.action.WasCompletedThisFrame())
        {
            magicMenu.SetActive(false);
            GameObject spMagic = null;
            hasMagic = true;
            switch (magicNum)
            {
                case 0:
                    if(!CheckMP()){ break; }
                    spMagic = Instantiate(magicSpown[magicNum], L_controller.transform.position, Quaternion.identity);
                    spMagic.GetComponent<MagicManager>().Getcontroller(L_controller);
                    player.GetComponent<PlayerData>().nowMP -= 10;
                    hasMagic = true;
                break;
                case 1:
                    break;
                case 2:
                    if(!CheckMP()){ break; }
                    spMagic = Instantiate(magicSpown[magicNum], L_controller.transform.position, Quaternion.identity);
                    spMagic.GetComponent<MagicManager>().Getcontroller(L_controller);
                    player.GetComponent<PlayerData>().nowMP -= 15;
                    hasMagic = true;
                    break;
                default:
                    break;
            }
        }
        
    }
    bool CheckMP()
    {
        if (player.GetComponent<PlayerData>().nowMP <= 0) { return false; }
        else{ return true; }
    }
    public void ShootedMagic()
    {
        hasMagic = false;
    }
    public void GameTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    
}
