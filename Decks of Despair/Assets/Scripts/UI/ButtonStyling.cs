using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ButtonStyling : MonoBehaviour
{
    [SerializeField] private float scaleSize; // Allows the developers to modify the scale size easily when testing
    private float originalSize; // Reference to the original size of button

    private void Start()
    {
        // Captures original size of button
        originalSize = transform.localScale.x;
    }
    public void HoverEnter()
    {
        // Transforms the scale to a slightly bigger button as the user hovers over it
        transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);                                                                                                                                                      
    }

    public void HoverLeave()
    {
        // Transforms the scale to a slightly smaller button as the user's cursor leaves it
        transform.localScale = new Vector3(originalSize, originalSize, originalSize);
    }
}
