using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

    [SerializeField] GameObject setupPanel;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject realSetupPanel;
    [SerializeField] GameObject SimulationSetupPanel;
    [SerializeField] GameObject LoodingPanel;

    private int loodingFlag = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
    }
    
    public void OnSimultionButtonClick()
    {
        //SceneManager.LoadScene("Simulation");
        if (loodingFlag == 0)
        {
            loodingFlag = 1;
            StartCoroutine("SimulationStart");
            startPanel.SetActive(false);
            LoodingPanel.SetActive(true);
        }
    }

    public void OnSetupButtonClick()
    {
        startPanel.SetActive(false);
        setupPanel.SetActive(true);
    }

    public void OnSetupBackButtonClick()
    {
        setupPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void OnSimulationSetupButtonClick()
    {
        realSetupPanel.SetActive(false);
        SimulationSetupPanel.SetActive(true);
    }

    public void OnRealSetupButtonClick()
    {
        realSetupPanel.SetActive(true);
        SimulationSetupPanel.SetActive(false);
    }

    IEnumerator SimulationStart()
    {
        // 次シーンを非同期で読み込むhttps://teratail.com/questions/216717
        var asyncOperation = SceneManager.LoadSceneAsync("Simulation");
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            Debug.Log("読み込み中..." + asyncOperation.progress + "%");
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
        //startPanel.SetActive(true);
        //LoodingPanel.SetActive(false);
        loodingFlag = 0;
    }
}
