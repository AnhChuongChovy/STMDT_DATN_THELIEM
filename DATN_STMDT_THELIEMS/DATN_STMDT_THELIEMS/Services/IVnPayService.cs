using DATN_STMDT_THELIEMS.Models.VnPay;

namespace DATN_STMDT_THELIEMS.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
