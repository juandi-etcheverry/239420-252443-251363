using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels.Responses.Purchases
{
    public class PurchaseDTO
    {
        public List<string> ProductsNames { get; set; } = new List<string>();
        public string UserEmail { get; set; }
        public float TotalPrice { get; set; }
        public float FinalPrice { get; set; }
        public string? PromotionName { get; set; }

        public static PurchaseDTO ToPurchaseDTO(Purchase purchase)
        {
            var purchaseDTO = new PurchaseDTO();
            purchaseDTO.UserEmail = purchase.User.Email;
            purchaseDTO.TotalPrice = purchase.TotalPrice;
            purchaseDTO.FinalPrice = purchase.FinalPrice;
            purchaseDTO.PromotionName = purchase.PromotionName;
            purchaseDTO.ProductsNames = purchase.Products.Select(p => p.Name).ToList();
            return purchaseDTO;
        }
    }
}
