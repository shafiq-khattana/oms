using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFom.Admin.Forms;
using WinFom.Common.Forms;

namespace WinFom.Common.Model
{
    public class SingleTon
    {
        private static LoginForm _loginForm;
        private static ShowAlarmForm _showAlarmForm;
        public static LoginForm LoginForm
        {
            get
            {
                if (_loginForm == null || _loginForm.IsDisposed)
                    _loginForm = new LoginForm();

                return _loginForm;
            }
        }
        public static ShowAlarmForm AlarmForm
        {
            get
            {
                if (_showAlarmForm == null || _showAlarmForm.Disposing)
                    _showAlarmForm = new ShowAlarmForm("");

                return _showAlarmForm;
            }
        }
    }
}
