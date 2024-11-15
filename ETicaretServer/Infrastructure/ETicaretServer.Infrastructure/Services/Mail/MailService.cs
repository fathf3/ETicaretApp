﻿using ETicaretServer.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Infrastructure.Services.Mail
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] {to}, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] to, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var item in to)
            {
                mail.To.Add(item);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"],"Fatih Mutlu Eticaret App", System.Text.Encoding.UTF8);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = "smtp.gmail.com";
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Merhaba <br> Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkden şifrenizi sıfırlayabilirsiniz. <br><stron> <a target=\"_blank\" href=\"");
            mail.Append(_configuration["ClientUrl"]);
            mail.Append("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifre talebi için tıklayınız... </a> </strong><br><br><span style=\"font-size:12px;\"> NOT: Eğer bu talebi siz yapmadıysanız bu mail i ciddiye almayınız.</span><br> Saygılarımızla...<br><br>ETicaretApp");

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
            
        }
    }
}