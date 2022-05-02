using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class mercatorSelection : MonoBehaviour
{

    public Texture2D selectionMap;
    public Camera cam;
    public Button play; 
    public Button validation; 
    public TextMeshProUGUI quest;
    public float deltaSleepTime;
    
    // private Color colorAustralia = new Color(192f/255f, 64f/255f, 128f/255f);
    // private Color colorEurope = new Color(193f/255f, 0f/255f, 0f/255f);
    private Color colorOcean = new Color(255f/255f, 255f/255f, 255f/255f);
    private Color colorLand = new Color(0f/255f, 0f/255f, 0f/255f);
    private string selection;
    private string state;
    private Vector3 mousePreviousPosition;
    private float sleepTime;


    void Start()
    {
        validation.onClick.AddListener(Validate);
        validation.gameObject.SetActive(false);
        play.onClick.AddListener(Play);
        play.gameObject.SetActive(true);
        quest.gameObject.SetActive(false);
        state = "before";
    }

    void BeforeSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePreviousPosition = Input.mousePosition;
            state = "select";
        }
    }

    void Select()
    {
        if (!Input.GetMouseButtonUp(0))
        {
            if (Input.GetMouseButton(0) && Input.mousePosition != mousePreviousPosition)
                {
                    state = "wait";
                }
            return;
        }
        state = "wait";
        validation.gameObject.SetActive(false);

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
        {
            return;
        }

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            return;
        }

        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= selectionMap.width;
        pixelUV.y *= selectionMap.height;
        
        ColorToString(selectionMap.GetPixel((int)pixelUV.x, (int)pixelUV.y));
    }

    void ColorToString(Color colorSelection)
    {
        if (colorSelection == colorOcean)
        {
            Debug.Log($"This is Ocean");
            selection = "Ocean";
            validation.gameObject.SetActive(true);
        }
        else if (colorSelection == colorLand)
        {
            Debug.Log($"This is Land");
            selection = "Land";
            validation.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log($"Measured value is {colorSelection}");
        }
    }

    void MakeQuest()
    {
        quest.text = "Find WATER\n(it's f***ing easy)";
        quest.gameObject.SetActive(true);
    }

    void Validate()
    {
        validation.gameObject.SetActive(false);
        if (selection == "Ocean")
        {
            quest.text = "WoW!\nYou surprisingly succeed";
        }
        else
        {
            quest.text = "WoW!\nYou stink, looser";
        }
        sleepTime = Time.time + deltaSleepTime;
        state = "sleep";
    }

    void Play()
    {
        play.gameObject.SetActive(false);
        MakeQuest();
        state = "wait";
    }

    void Update()
    {
        switch (state)
        {
            case "wait":
                BeforeSelect();
                break;

            case "select":
                Select();
                break;

            case "after":
                Start();
                quest.gameObject.SetActive(true);
                break;

            case "sleep":
                if (Time.time > sleepTime)
                {
                    state = "after";
                }
                break;

            default:
                break;
        }
    }
}