﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachRepository.common
{
    public class SelectListItem
    {
        public SelectListItem() { }

        public bool Disabled { get; set; }
        public SelectListGroup Group { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
