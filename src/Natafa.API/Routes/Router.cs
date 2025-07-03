using Natafa.Domain.Entities;

namespace Natafa.Api.Routes
{
    public static class Router
    {
        public const string Id = "{" + "id" + "}";
        public const string prefix = "api/";

        public static class AtuthenticationRoute
        {
            public const string Authentication = $"{prefix}authen";
            public const string Register = $"{Authentication}/register";
            public const string Login = $"{Authentication}/login";
            public const string VerifyEmail = $"{Authentication}/verify-email";
            public const string RefreshToken = $"{Authentication}/refresh-token";
        }

        public static class UserRoute
        {
            public const string Users = $"{prefix}users";
            public const string GetUpdateDelete = $"{Users}/{Id}";
            public const string GetUpdateDeleteProfile = $"{Users}/profile";
            public const string CreateUser = $"{Users}"; 
            public const string UpdateUserStatus = $"{Users}/status/{Id}"; // Cập nhật trạng thái user
            public const string GetUsersByVoucherId = $"{Users}/" + "{" + "voucherId" + "}";
        }

        public static class OrderRoute
        {
            public const string Orders = $"{prefix}orders";
            public const string CreateOrder = $"{Orders}";
            public const string ConfirmOrder = $"{Orders}/confirm";
            public const string CompleteOrder = $"{Orders}/complete";
            public const string CancelOrder = $"{Orders}/cancel";
            public const string GetUserOrder = $"{Orders}/me";
            public const string GetAllOrder = $"{Orders}";
            public const string GetShippingCost = $"{Orders}/shipping-cost";
        }

        public static class PaymentRoute
        {
            public const string Payment = $"{prefix}payment";
            public const string MakePayment = $"{Payment}/payment";
            public const string AddToWallet = $"{Payment}/wallet-payment";
            public const string PaymentCallBack = $"{Payment}/payment-call-back";
        }

        public static class ExcelRoute
        {
            public const string Excel = $"{prefix}excels";
            public const string GetMonthlyRevenueExcel = $"{Excel}/monthly-revenue-excel";
            public const string GetQuarterlyRevenueExcel = $"{Excel}/quarterly-revenue-excel";
            public const string GetYearlyRevenueExcel = $"{Excel}/yearly-revenue-excel";
        }

        public static class CategoryRoute
        {
            public const string Categories = $"{prefix}categories";
            public const string GetCategory = $"{Categories}/{Id}";
            public const string GetCategories = $"{Categories}";
            public const string CreateCategory = $"{Categories}";
            public const string UpdateCategory = $"{Categories}/{Id}";
            public const string DeleteCategory = $"{Categories}/{Id}";
        }

        public static class ProductRoute
        {
            public const string Products = $"{prefix}products";
            public const string GetProduct = $"{Products}/{Id}";
            public const string GetProducts = $"{Products}";
            public const string GetBestSellerProducts = $"{Products}/best-seller";
            public const string GetProductDetail = $"{GetProduct}/detail";
            public const string CreateProduct = $"{Products}";
            public const string UpdateProduct = $"{Products}/{Id}";
            public const string DeleteProduct = $"{Products}/{Id}";
        }

        public static class TransactionRoute
        {
            public const string Transactions = $"{prefix}transactions";
            public const string GetAllTransactions = $"{Transactions}";
            public const string GetUserTransactions = $"{Transactions}/me";
            public const string GetTransaction = $"{Transactions}/{Id}";
        }

        public static class FeedbackRoute
        {
            public const string Feedbacks = $"{prefix}feedbacks";            
            public const string GetAllFeedbacks = $"{Feedbacks}";
            public const string CreateFeedback = $"{Feedbacks}";
            public const string GetUpdateDelete = $"{Feedbacks}/{Id}";
            public const string GetFeedbackByProduct = $"{Feedbacks}/{{productId}}";
        }

        public static class VoucherRoute
        {
            private const string Vouchers = $"{prefix}vouchers";
            public const string GetUpdateDelete = $"{Vouchers}/{Id}"; 
            public const string GetVouchers = $"{Vouchers}";
            public const string GetMyVouchers = $"{Vouchers}/me";
            public const string GetVouchersByUserId = $"{Vouchers}/staff/" +"{" + "userId" + "}";
            public const string CreateVoucher = $"{Vouchers}"; 
            public const string ExpiredVoucher = $"{Vouchers}/expired-vouchers";
            public const string TakeVoucher = $"{Vouchers}/{Id}/take";
        }

        public static class UserVoucherRoute
        {
            private const string prefix = "api/user-voucher";
            public const string GetUserVoucherById = $"{prefix}/{{id}}"; // Lấy thông tin UserVoucher theo ID
            public const string GetUserVouchersByUserId = $"{prefix}/user/{{userId}}"; // Lấy danh sách voucher của khách hàng
            public const string AssignVoucher = $"{prefix}/assign"; // Assign voucher cho khách hàng
            public const string RedeemVoucher = $"{prefix}/redeem/{{id}}"; // Đổi voucher
        }

        public static class DashboardRoute
        {
            public const string Dashboard = $"{prefix}dashboard";
            public const string GetMonthlyRevenue = $"{Dashboard}/monthly-revenue";
            public const string GetQuarterlyRevenue = $"{Dashboard}/quarterly-revenue";
            public const string GetYearlyRevenue = $"{Dashboard}/yearly-revenue";
            public const string GetMonthlyTotalFeedbacks = $"{Dashboard}/monthly-total-feedbacks";
            public const string GetQuarterlyTotalFeedbacks = $"{Dashboard}/quarterly-total-feedbacks";
            public const string GetYearlyTotalFeedbacks = $"{Dashboard}/yearly-total-feedbacks";
        }

        public static class WishListRoute
        {
            public const string WishList = $"{prefix}wish-list";
            public const string GetWishList = $"{WishList}";
            public const string AddToWishList = $"{WishList}/{Id}";
        }

        public static class ShippingAddressRoute
        {
            public const string ShippingAddress = $"{prefix}shippingAddresses";
            public const string GetUpdateDelete = $"{ShippingAddress}/{Id}";
            public const string GetShippingAddresses = $"{ShippingAddress}";
            public const string CreateShippingAddress = $"{ShippingAddress}";
        }
    }
}
