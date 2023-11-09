using ApiModels.Responses.Promotions;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.User.Admin;

namespace WebApi.Controllers.Promotions;

[ApiController]
[Route("api/promotions")]
public class PromotionsController : ControllerBase
{
   private IPromotionLogic _promotionLogic;
   private IProductLogic _productLogic;
   
   public PromotionsController(IPromotionLogic promotionLogic, IProductLogic productLogic)
   {
      _promotionLogic = promotionLogic;
      _productLogic = productLogic;
   }

   [HttpPost]
   [Route("discount")]
   public IActionResult GetPromotionForProducts([FromBody] PurchaseProduct[] products)
   {
      var foundProducts = new List<Product>();
      foreach (var p in products)
      {
         var foundProduct = _productLogic.GetProduct(p.ProductId);
         for (int i = 0; i < p.Quantity; i++)
         {
            foundProducts.Add(foundProduct);
         }
      }

      var promotion = _promotionLogic.GetBestPromotion(foundProducts);
      var discount = promotion.GetDiscount(foundProducts);
      var totalPrice = foundProducts.Sum(p => p.Price);
      return StatusCode(200, new GetPromotionResponse()
      {
         PromotionName = promotion.Name,
         Discount = discount,
         FinalPrice = totalPrice - discount
      });
   }

   [HttpPost]
   [Route("{name}/toggle")]
   [ServiceFilter(typeof(AdminUserAuthenticationFilter))]
   public IActionResult TogglePromotions([FromRoute] string name)
   {
      _promotionLogic.TogglePromotion(name);
      return StatusCode(200);
   }

   [HttpGet]
   public IActionResult GetAllPromotions()
   {
      var promotionDTOs = _promotionLogic.GetAllPromotions().Select(p => new PromotionDTO()
      {
         Name = p.Name,
         IsEnabled = p.IsEnabled
      }).ToList();
      return StatusCode(200, new GetPromotionsResponse()
      {
         Promotions = promotionDTOs
      });
   }
}