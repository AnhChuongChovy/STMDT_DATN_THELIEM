using DATN_STMDT_THELIEMS.Models;
using DATN_STMDT_THELIEMS.Models.Momo;

namespace DATN_STMDT_THELIEMS.Services
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponse> CreatePaymentAsync(Checkout checkoutModel, int userId, string momoOrderId);
        MomoExecuteResponse PaymentExecuteAsync(IQueryCollection collection);
    }
}
