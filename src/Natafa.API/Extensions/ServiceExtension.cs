using Natafa.Api.Services.Implements;
using Natafa.Api.Services.Interfaces;
using Natafa.Domain.Entities;
using Natafa.Repository.Implement;
using Natafa.Repository.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Mail;

namespace Natafa.Api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddService(this IServiceCollection service)
        {
            service.AddTransient<IUnitOfWork, UnitOfWork<NatafaDbContext>>();
            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            service.AddTransient<IAuthenticationService, AuthenticationService>();
            service.AddTransient<IRefreshTokenService, RefreshTokenService>();
            service.AddTransient<IEmailService, EmailService>();
            service.AddTransient<ISmtpClient, MailKit.Net.Smtp.SmtpClient>();
            service.AddTransient<ITokenGenerator, TokenGenerator>();
            service.AddTransient<ITokenValidator, TokenValidator>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<IOrderService, OrderService>();
            service.AddTransient<IShippingService, ShippingService>();
            //service.AddTransient<IPaymentService, PaymentService>();
            service.AddTransient<IExcelService, ExcelService>();
            service.AddTransient<ICloudinaryService, CloudinaryService>();
            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<ITransactionService, TransactionService>();
            service.AddTransient<IDashboardService, DashboardService>();
            service.AddTransient<IVoucherService, VoucherService>();
            service.AddTransient<IFeedbackService, FeedbackService>();
            service.AddTransient<IWishListService, WishListService>();
            service.AddTransient<IShippingAddressService, ShippingAddressService>();

            return service;
        }
    }
}
