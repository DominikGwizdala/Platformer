using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changepositionSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentPosition;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //Zmiana pozycji strza�ki
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            ChangePosition(-1);

        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);

        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            Interact();

    }
    private void ChangePosition(int _change)
    {

        currentPosition += _change;
        if (_change != 0) 
            SoundManager.instance.PlaySound(changepositionSound);
         
        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if(currentPosition>options.Length-1)
            currentPosition = 0;

        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        options[currentPosition].GetComponent<Button>().onClick.Invoke();//Nie dzia�a na silniku 
        
    }
}
