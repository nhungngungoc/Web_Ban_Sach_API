﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Entity;

namespace WebBanSachModel.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string TenSP { get; set; }
        public string? Image { get; set; }
        public string MoTa { get; set; }
        public long Gia { get; set; }
        public string TenTacGia { get; set; }
        public Guid CategoryId { get; set; }
    }
}
