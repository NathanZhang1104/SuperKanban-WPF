using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace SuperKanban.Model.Control
{
    public class AppProcess:Process
    {
        private string url = null;
        public string Url { get => url; set => url = value; }
    }
}
