using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [MinLength(5)]
        [MaxLength(100)]
        public string HoVaTen { get; set; }
        [MinLength(5)]
        public string MatKhau { get; set; }
        [MinLength(5)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string? NgaySinh { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
        public string Quyen { get; set; }
    }
}
