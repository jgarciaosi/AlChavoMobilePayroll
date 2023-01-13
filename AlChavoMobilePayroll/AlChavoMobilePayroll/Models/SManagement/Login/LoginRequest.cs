using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.SManagement.Login
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }        
        //Compañía logueada anterior
        public string CompId { get; set; }

    }

}
