using DATN_STMDT_THELIEMS.Models;
using DATN_STMDT_THELIEMS.Models.Momo;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using DATN_STMDT_THELIEMS.DATA;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace DATN_STMDT_THELIEMS.Services
{
    public class MomoService : IMomoService
    {
        private readonly IOptions<MomoOption> _options;
        private readonly AppDBContext _context;
        private readonly IUserService _userService;

        public MomoService(IOptions<MomoOption> options, AppDBContext context, IUserService userService)
        {
            _options = options;
            _context = context;
            _userService = userService;
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }

        public async Task<MomoCreatePaymentResponse> CreatePaymentAsync(Checkout checkoutModel, int userId, string momoOrderId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy người dùng.");
            }

            // Chuẩn bị thông tin thanh toán và orderId cho MoMo
            string orderInfo = $"Khách hàng: {user.Full_name}. Nội dung: Thanh toán đơn hàng #{momoOrderId}.";

            // Tạo dữ liệu raw cho yêu cầu thanh toán
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={momoOrderId}&amount={checkoutModel.TotalPayment}&orderId={momoOrderId}&orderInfo={orderInfo}&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData=";

            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

            // Gửi yêu cầu đến MoMo
            var client = new RestClient(_options.Value.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrl,
                orderId = momoOrderId, 
                amount = checkoutModel.TotalPayment.ToString(),
                orderInfo = orderInfo,
                requestId = momoOrderId,  
                extraData = "",
                signature = signature
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            var momoResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResponse>(response.Content);
            Console.WriteLine($"Phản hồi từ MoMo: {response.Content}");

            return momoResponse;
        }

        public MomoExecuteResponse PaymentExecuteAsync(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            return new MomoExecuteResponse()
            {
                Amount = amount,
                OrderId = orderId,
                OrderInfo = orderInfo
            };
        }
    }
}
