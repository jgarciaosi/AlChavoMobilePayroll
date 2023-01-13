using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.CheckRegister
{
    public class ModelCheckRegisterLastPie
    {
        public string TitlePie { get; set; }

        public double ValuePie { get; set; }

        public ModelCheckRegisterLastPie(string xValue, double yValue)
        {
            TitlePie = xValue;
            ValuePie = yValue;
        }

    }
}
