using DATN_STMDT_THELIEMS.Models;
using Google.Cloud.Firestore;

namespace DATN_STMDT_THELIEMS.Services
{
    public class FirestoreService
    {
        private readonly FirestoreDb _firestoreDb;

        public FirestoreService()
        {
            // Tên project Firebase của bạn
            string projectId = "theliems-b77a4";
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        // Thêm sản phẩm vào giỏ hàng
        public async Task AddToCartAsync(string userId, Checkout product)
        {
            try
            {
                DocumentReference docRef = _firestoreDb
                    .Collection("users")
                    .Document(userId)
                    .Collection("cart")
                    .Document(product.ProductId.ToString());

                await docRef.SetAsync(new
                {
                    product.ProductId,
                    product.Image,
                    product.Color,
                    product.Size,
                    product.Quantity,
                    product.DiscountedPrice,
                    product.Price
                });

                Console.WriteLine("Product added to cart.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product to cart: {ex.Message}");
            }
        }

        // Lấy giỏ hàng
        public async Task<List<Checkout>> GetCartAsync(string userId)
        {
            List<Checkout> cart = new List<Checkout>();

            try
            {
                Query query = _firestoreDb
                    .Collection("users")
                    .Document(userId)
                    .Collection("cart");

                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    cart.Add(doc.ConvertTo<Checkout>());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching cart: {ex.Message}");
            }

            return cart;
        }

        // Xóa sản phẩm khỏi giỏ hàng
        public async Task RemoveFromCartAsync(string userId, string productId)
        {
            try
            {
                DocumentReference docRef = _firestoreDb
                    .Collection("users")
                    .Document(userId)
                    .Collection("cart")
                    .Document(productId);

                await docRef.DeleteAsync();
                Console.WriteLine("Product removed from cart.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing product: {ex.Message}");
            }
        }
    }
}
