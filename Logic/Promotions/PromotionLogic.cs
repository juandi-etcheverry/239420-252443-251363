using Domain;
using Logic.Interfaces;

namespace Logic;

public class PromotionLogic : IPromotionLogic
{
    private static IList<CachedPromotion> _cachedPromotions = new List<CachedPromotion>();
    private string PROMOTIONS_DIRECTORY = "./Promotions";
    private static DateTime _promotionsLastModified;
    private IFileDataReader _fileDataReader;
    
    public PromotionLogic(IFileDataReader fileDataReader)
    {
        _fileDataReader = fileDataReader;
        VerifyPromotions();
    }

    public IPromotionStrategy GetBestPromotion(List<Product> products)
    {
        VerifyPromotions();
        var enabledPromotions = _cachedPromotions.Where(c => c.IsEnabled);
        var promotions = enabledPromotions.Select(c => c.PromotionStrategy);
        var bestPromotion = promotions.MaxBy(st => st.GetDiscount(products));
        if (bestPromotion == null || bestPromotion.GetDiscount(products) <= 0)
            throw new ArgumentException("No promotion is applicable to these products");
        return bestPromotion;
    }
    
    public void ForceRefresh()
    {
        _promotionsLastModified = DateTime.MinValue;
        _cachedPromotions.Clear();
        UpdatePromotions();
    }
    
    public bool TogglePromotion(string name)
    {
        var promotion = _cachedPromotions.FirstOrDefault(c => c.PromotionStrategy.Name == name);
        if (promotion == null)
            throw new ArgumentException("No promotion with that name exists");
        promotion.IsEnabled = !promotion.IsEnabled;
        return promotion.IsEnabled;
    }

    private void VerifyPromotions()
    {
        var currentLastModified = _fileDataReader.GetLastModified(PROMOTIONS_DIRECTORY);
        if (currentLastModified.ToLongDateString() != _promotionsLastModified.ToLongDateString())
        {
            _promotionsLastModified = currentLastModified;
            UpdatePromotions();
            _cachedPromotions = _cachedPromotions.OrderBy(c => c.PromotionStrategy.Name).ToList();
        }
    }

    private void UpdatePromotions()
    {
        var filePaths = _fileDataReader.GetDirectoryFilePaths(PROMOTIONS_DIRECTORY);
        RemoveOrVerifyCachedPromotions(filePaths);
        AddNewPromotionsFromDirectory(filePaths); 
    }
    
    private void AddNewPromotionsFromDirectory(string[] filePaths)
    {
        foreach (var path in filePaths)
        {
            if (_cachedPromotions.All(c => c._filePath != path))
            {
                var cachedPromotion = new CachedPromotion()
                {
                    _filePath = path
                };
                cachedPromotion.Verify();
                _cachedPromotions.Add(cachedPromotion);
            }
        }
    }

    private void RemoveOrVerifyCachedPromotions(string[] filePaths)
    {
        foreach (var cachedPromotion in _cachedPromotions)
        {
            if (!filePaths.Contains(cachedPromotion._filePath))
            {
                _cachedPromotions. Remove(cachedPromotion);
            }
            else
            {
                cachedPromotion.Verify();
            }
        }
    }
}