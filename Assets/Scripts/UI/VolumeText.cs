
using UnityEngine;
using UnityEngine.UI;
public class VolumeText : MonoBehaviour
{
    [SerializeField]private string volumeNames;
    [SerializeField] private string TextIntro;
    private Text txt;

    private void Awake()
    {
        
        txt = GetComponent<Text>();
    }
    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeNames) * 100;
        txt.text = TextIntro + volumeValue;
    }
}
