﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.OilDealManaged.Reports.Model
{
    public class RCOCompRVM
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte[] Logo { get; set; }
        public string RepDate { get; set; }
        public string RepTitle { get; set; }
        public byte[] Qrcode { get; set; }
        public byte[] Barcode { get; set; }
        
    }
}
