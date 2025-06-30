using Natafa.Api.Constants;
using Natafa.Api.Routes;
using Natafa.Api.Services.Implements;
using Natafa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Natafa.Api.Controllers
{
    /// <summary>
    /// Controller quản lý Dashboard, dành riêng cho Admin.
    /// </summary>
    [Authorize(Roles = UserConstant.USER_ROLE_ADMIN)]
    public class DashboardController : BaseApiController
    {
        private readonly IExcelService _excelService;
        private readonly IDashboardService _dashboardService;

        public DashboardController(IExcelService excelService, IDashboardService dashboardService)
        {
            _excelService = excelService;
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Lấy dữ liệu tổng quan Dashboard.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Admin.
        /// </remarks>
        /// <returns>Dữ liệu tổng quan Dashboard.</returns>
        [HttpGet]
        [Route(Router.DashboardRoute.Dashboard)]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _dashboardService.GetDashboardAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy doanh thu theo tháng.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Admin.
        /// </remarks>
        /// <returns>Dữ liệu doanh thu theo tháng.</returns>
        [HttpGet]
        [Route(Router.DashboardRoute.GetMonthlyRevenue)]
        public async Task<IActionResult> GetMonthlyRevenue()
        {
            var result = await _excelService.GetRevenueByMonthAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lấy doanh thu theo quý.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Admin.
        /// </remarks>
        /// <returns>Dữ liệu doanh thu theo quý.</returns>
        [HttpGet]
        [Route(Router.DashboardRoute.GetQuarterlyRevenue)]
        public async Task<IActionResult> GetQuarterlyRevenue()
        {
            var result = await _excelService.GetRevenueByQuarterAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lấy doanh thu theo năm.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Admin.
        /// </remarks>
        /// <returns>Dữ liệu doanh thu theo năm.</returns>
        [HttpGet]
        [Route(Router.DashboardRoute.GetYearlyRevenue)]
        public async Task<IActionResult> GetYearlyRevenue()
        {
            var result = await _excelService.GetRevenueByYearAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lấy tổng số feedback theo tháng.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Admin.
        /// </remarks>
        /// <returns>Số lượng feedback theo tháng.</returns>
        [HttpGet]
        [Route(Router.DashboardRoute.GetMonthlyTotalFeedbacks)]
        public async Task<IActionResult> GetMonthlyTotalFeedbacks()
        {
            var result = await _dashboardService.GetTotalFeedbacksByMonthAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy tổng số feedback theo quý.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Admin.
        /// </remarks>
        /// <returns>Số lượng feedback theo quý.</returns>
        [HttpGet]
        [Route(Router.DashboardRoute.GetQuarterlyTotalFeedbacks)]
        public async Task<IActionResult> GetQuarterlyTotalFeedbacks()
        {
            var result = await _dashboardService.GetTotalFeedbacksByQuarterAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        /// <summary>
        /// Lấy tổng số feedback theo năm.
        /// </summary>
        /// <remarks>
        /// Yêu cầu Role: Admin.
        /// </remarks>
        /// <returns>Số lượng feedback theo năm.</returns>
        [HttpGet]
        [Route(Router.DashboardRoute.GetYearlyTotalFeedbacks)]
        public async Task<IActionResult> GetYearlyTotalFeedbacks()
        {
            var result = await _dashboardService.GetTotalFeedbacksByYearAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
