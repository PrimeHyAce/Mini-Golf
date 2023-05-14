using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook CMfreeLook;
    public void SetInputActive(bool value)
    {
        if (value)
        {
            CMfreeLook.m_XAxis.m_InputAxisName = "Mouse X";
            CMfreeLook.m_YAxis.m_InputAxisName = "Mouse Y";  
        }
        else
        {
            CMfreeLook.m_XAxis.m_InputAxisName = "";
            CMfreeLook.m_YAxis.m_InputAxisName = ""; 
             
            CMfreeLook.m_XAxis.m_InputAxisValue = 0;
            CMfreeLook.m_YAxis.m_InputAxisValue = 0;
        }
    }
}
