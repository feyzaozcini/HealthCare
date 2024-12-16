﻿using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Helpers;

public static class TestResultCalculator
{
    public static (string TextStyle, string TextClass, string Icon, string Text) GetResultStatus(TestProcessDto testProcess)
    {
        var result = testProcess.Result;
        var minValue = testProcess.TestMinValue;
        var maxValue = testProcess.TestMaxValue;

        string textStyle = "font-weight: normal;";
        string textClass = "";
        string icon = "";
        string text = "";

        if (result < minValue)
        {
            textStyle = "font-weight: bold; color: red;";
            textClass = "low";
            icon = "⚠"; 
            text = "Düşük";
        }
        else if (result > maxValue)
        {
            textStyle = "font-weight: bold; color: red;";
            textClass = "high";
            icon = "⚠"; 
            text = "Yüksek";
        }
        else
        {
            textStyle = "font-weight: bold; color: green;";
            textClass = "normal";
            icon = "✔"; 
            text = "Normal";
        }

        return (textStyle, textClass, icon, text);
    }
}
