using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void ShowPanel()
    {
        howToPlayPanel.SetActive(true);
    }

    public void HidePanel()
    {
        howToPlayPanel.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
