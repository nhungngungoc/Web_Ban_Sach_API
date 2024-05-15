using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WebBanSachAPI.Controllers.Pay
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private readonly IVnPayService _vnPayservice;
        public PayController(IVnPayService vnPayservice)
        {
            _vnPayservice = vnPayservice;
        }
        [HttpGet]
        public IActionResult Get() 
        {
            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = 200000,
                CreatedDate = DateTime.Now,
                Description = "Trịnh Công Triệu",
                FullName ="Trịnh Công Triệu",
                OrderId = new Random().Next(1000, 100000)
            };
            return Ok(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
        }

    }
}

public class Utils
{
    public static string HmacSHA512(string key, string inputData)
    {
        var hash = new StringBuilder();
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var inputBytes = Encoding.UTF8.GetBytes(inputData);
        using (var hmac = new HMACSHA512(keyBytes))
        {
            var hashValue = hmac.ComputeHash(inputBytes);
            foreach (var theByte in hashValue)
            {
                hash.Append(theByte.ToString("x2"));
            }
        }

        return hash.ToString();
    }


    // có chế biến cho .NET Core MVC
    public static string GetIpAddress(HttpContext context)
    {
        var ipAddress = string.Empty;
        try
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress;

            if (remoteIpAddress != null)
            {
                if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                        .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                }

                if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

                return ipAddress;
            }
        }
        catch (Exception ex)
        {
            return "Invalid IP:" + ex.Message;
        }

        return "127.0.0.1";
    }
}
