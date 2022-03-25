using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowControl : MonoBehaviour
{
    [SerializeField] GameObject background;

    [Header("Shader Properties Reference")]
    [SerializeField] string MainTexColorString;
    [SerializeField] string colorStringEmission;
    [SerializeField] string glowVectorString = "_Emission_Multiplier";

    [Header("Glow Vectors")]
    [SerializeField] Vector3 glowStart = new Vector3(0f, 0f, 0f);
    [SerializeField] Vector3 glowMax = new Vector3(2f, 2f, 2f);
    [SerializeField] Vector3 glowEnd = new Vector3(2f, 2f, 2f);

    [Header("Glow Color")]
    [SerializeField] Color colorEmissionStart;
    [SerializeField] Color colorEmissionEnd;

    [Header("Texture Color")]
    [SerializeField] Color colorMainTexStart;
    [SerializeField] Color colorMainTexEnd;

    Material glowMaterial;
    bool buttonChange = true;

    enum State { Standing, NotStanding, ChangeColor, DimingUp, DimingDown }
    State masterState = State.Standing;
    State mainTexColorState = State.Standing;
    State emissiveColorState = State.Standing;
    State emissiveGlowState = State.Standing;

    [SerializeField] float glowChangeSpeed = 0.1f;
    [SerializeField] float colorChangeSpeed = 0.1f;

    Vector3 glowChangeTemp;
    [SerializeField] Color emissColorTemp;
    [SerializeField] Color mainTexColorTemp;

    private void Start()
    {
        glowMaterial = background.GetComponent<SpriteRenderer>().material;
        glowMaterial.SetVector(glowVectorString, glowStart);
        glowMaterial.SetColor(colorStringEmission, colorEmissionStart);
        glowMaterial.SetColor(MainTexColorString, colorMainTexStart);

        glowChangeTemp = new Vector3(glowStart.x, glowStart.y, glowStart.z);
        emissColorTemp = colorEmissionStart;
        mainTexColorTemp = colorMainTexStart;
    }

    private void Update()
    {
        if (masterState != State.Standing)
        {
            ChangeBackgroundColor();
        }
    }

    public void ChangeColorTrigger()
    {
        masterState = State.NotStanding;
        mainTexColorState = State.ChangeColor;
        emissiveGlowState = State.DimingUp;
        emissiveColorState = State.ChangeColor;
    }

    public void ChangeBackgroundColor()
    {
        //  DISCLAIMER!!: Color change logic works with a white color as start (max value of RGB´s)
        ChangeTexColor();
        ChangeEmissColor();
        ChangeGlow();

        if (mainTexColorState == State.Standing && emissiveGlowState == State.Standing && emissiveColorState == State.Standing)
        {
            masterState = State.Standing;
        }
    }

    private void ChangeEmissColor()
    {
        if (emissColorTemp.r > colorEmissionEnd.r)
        {
            emissColorTemp.r = emissColorTemp.r - Time.deltaTime * colorChangeSpeed;
        }        
        else { emissColorTemp.r = colorEmissionEnd.r; }

        if (emissColorTemp.g > colorEmissionEnd.g)
        {
            emissColorTemp.g = emissColorTemp.g - Time.deltaTime * colorChangeSpeed;
        }
        else { emissColorTemp.g = colorEmissionEnd.g; }

        if (emissColorTemp.b > colorEmissionEnd.b)
        {
            emissColorTemp.b = emissColorTemp.b - Time.deltaTime * colorChangeSpeed;
        }
        else { emissColorTemp.b = colorEmissionEnd.b; }

        glowMaterial.SetColor(colorStringEmission, emissColorTemp);
        if (emissColorTemp == colorEmissionEnd)
        {
            emissiveColorState = State.Standing;            
        }
    }

    private void ChangeGlow()
    {
        if (emissiveGlowState == State.DimingUp)
        {
            if (glowChangeTemp.x < glowMax.x)
            {
                glowChangeTemp = new Vector3(glowChangeTemp.x + Time.deltaTime * glowChangeSpeed, glowChangeTemp.y + Time.deltaTime * glowChangeSpeed, glowChangeTemp.z + Time.deltaTime * glowChangeSpeed);
            }
            else { emissiveGlowState = State.DimingDown; }
        }

        else if (emissiveGlowState == State.DimingDown)
        {
            if (glowChangeTemp.x > glowEnd.x)
            {
                glowChangeTemp = new Vector3(glowChangeTemp.x - Time.deltaTime * glowChangeSpeed, glowChangeTemp.y - Time.deltaTime * glowChangeSpeed, glowChangeTemp.z - Time.deltaTime * glowChangeSpeed);
            }
            else
            {
                emissiveGlowState = State.Standing;                
                glowChangeTemp = new Vector3(glowEnd.x, glowEnd.y, glowEnd.z);
            }
        }
        glowMaterial.SetVector(glowVectorString, glowChangeTemp);
    }

    private void ChangeTexColor()
    {
        if (mainTexColorTemp.r > colorMainTexEnd.r)
        {
            mainTexColorTemp.r = mainTexColorTemp.r - Time.deltaTime * colorChangeSpeed;
        }
        else { mainTexColorTemp.r = colorMainTexEnd.r; }

        if (mainTexColorTemp.g > colorEmissionEnd.g)
        {
            mainTexColorTemp.g = mainTexColorTemp.g - Time.deltaTime * colorChangeSpeed;
        }
        else { mainTexColorTemp.g = colorMainTexEnd.g; }

        if (mainTexColorTemp.b > colorMainTexEnd.b)
        {
            mainTexColorTemp.b = emissColorTemp.b - Time.deltaTime * colorChangeSpeed;
        }
        else { mainTexColorTemp.b = colorMainTexEnd.b; }

        glowMaterial.SetColor(MainTexColorString, mainTexColorTemp);

        if (mainTexColorTemp == colorMainTexEnd)
        {            
            mainTexColorState = State.Standing;
        }
    }

    public void ButtonTest()
    {
        if (buttonChange)
        {
            glowMaterial.SetVector(glowVectorString, glowStart);
            glowMaterial.SetColor(colorStringEmission, colorEmissionStart);
            glowMaterial.SetColor(MainTexColorString, colorMainTexStart);
            buttonChange = false;
        }
        else
        {
            glowMaterial.SetVector(glowVectorString, glowEnd);
            glowMaterial.SetColor(colorStringEmission, colorEmissionEnd);
            glowMaterial.SetColor(MainTexColorString, colorMainTexEnd);
            buttonChange = true;
        }
    }
}
