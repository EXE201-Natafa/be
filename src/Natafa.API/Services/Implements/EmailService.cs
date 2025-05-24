using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Natafa.Api.Models.Configurations;
using Natafa.Api.Services.Interfaces;
using Natafa.Api.Models.AuthenticationModel;
using Natafa.Api.Helper;
using Natafa.Domain.Entities;

namespace Natafa.Api.Services.Implements
{
    public class EmailService : IEmailService
    {
        private readonly MailConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly LinkGenerator _linkGenerator;
        private readonly ISmtpClient _smtpClient;

        public EmailService(IOptions<MailConfiguration> options,
            ITokenGenerator tokenGenerator,
            LinkGenerator linkGenerator,
            ISmtpClient smtpClient)
        {
            _configuration = options.Value;
            _tokenGenerator = tokenGenerator;
            _linkGenerator = linkGenerator;
            _smtpClient = smtpClient;
        }

        private async Task<bool> SendMailAsync(MailData mailData)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.SenderName, _configuration.FromEmail));
            message.To.Add(new MailboxAddress(mailData.ReceiverName, mailData.ToEmail));
            message.Subject = mailData.Subject;
            message.Body = new TextPart(TextFormat.Html) { Text = mailData.Body };

            try
            {
                await _smtpClient.ConnectAsync(_configuration.Server, _configuration.Port, SecureSocketOptions.StartTls);
                await _smtpClient.AuthenticateAsync(_configuration.FromEmail, _configuration.Password);
                await _smtpClient.SendAsync(message);
                await _smtpClient.DisconnectAsync(true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<MethodResult<string>> SendAccountVerificationEmailAsync(User user)
        {
            var token = await _tokenGenerator.GenerateEmailVerificationToken(user.Email);
            var link = $"https://localhost:7105/api/Authentication/verify-email?token={token}";
            var content = @$"User {user.FullName} Register";
            var des = "Click to confirm your account";
            var mail = new MailData
            {
                ReceiverName = user.FullName,
                ToEmail = user.Email,
                Subject = $"[{_configuration.SenderName}] {(user.Role)} Account Registration Verification",
                Body = EmailTemplates.SendMailWithLink(content, des, link)
            };
            var emailSent = await SendMailAsync(mail);
            if (!emailSent)
            {
                return new MethodResult<string>.Failure("Failed to send verification email", StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Please check your email for account verification link");
        }
    }
}
