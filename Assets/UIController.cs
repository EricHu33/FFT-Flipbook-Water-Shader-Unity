using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TileValue;
    public TMPro.TextMeshProUGUI DisplacementValue;
    public TMPro.TextMeshProUGUI NormalStrengthValue;
    public TMPro.TextMeshProUGUI FramerateValue;
    public Material Mat;
    public Transform Sun;

    private int originTileValue;

    private float originDisplacementValue;
    private float originNormalStrengthValue;
    private int originFramerateValue;

    public void OnTileChange(float value)
    {
        var intValue = (int)value;
        Mat.SetFloat("_Tile", (float)intValue);
        TileValue.text = "" + intValue;
    }


    public void OnDisplacementChange(float value)
    {
        Mat.SetFloat("_Displacement", value);
        DisplacementValue.text = "" + value;
    }


    public void OnNormalStrengthChange(float value)
    {
        Mat.SetFloat("_NormalStrength", value);
        NormalStrengthValue.text = "" + value;
    }

    public void OnFramerateChange(float value)
    {
        Mat.SetInt("_FrameRate", (int)value);
        FramerateValue.text = "" + value;
    }

    public void OnSunRotateSliderChange(float value)
    {
        Sun.localRotation = Quaternion.Euler(value, -value * 0.5f, 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        originDisplacementValue = Mat.GetFloat("_Displacement");
        originNormalStrengthValue = Mat.GetFloat("_NormalStrength");
        originFramerateValue = Mat.GetInt("_FrameRate");
        originTileValue = (int)Mat.GetFloat("_Tile");

        TileValue.text = "" + (int)Mat.GetFloat("_Tile");
        TileValue.GetComponentInParent<Slider>().value = originTileValue;

        DisplacementValue.text = "" + Mat.GetFloat("_Displacement");
        DisplacementValue.GetComponentInParent<Slider>().value = originDisplacementValue;

        NormalStrengthValue.text = "" + Mat.GetFloat("_NormalStrength");
        NormalStrengthValue.GetComponentInParent<Slider>().value = originNormalStrengthValue;

        FramerateValue.text = "" + Mat.GetInt("_FrameRate");
        FramerateValue.GetComponentInParent<Slider>().value = originFramerateValue;
    }

    private void OnDestroy()
    {
        Mat.SetFloat("_Tile", originTileValue);
        Mat.SetFloat("_Displacement", originDisplacementValue);
        Mat.SetFloat("_NormalStrength", originNormalStrengthValue);
        Mat.SetInt("_FrameRate", (int)originFramerateValue);
    }

}
