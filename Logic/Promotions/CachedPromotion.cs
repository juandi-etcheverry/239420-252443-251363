using System.Reflection;
using Logic.Interfaces;

namespace Logic;

public class CachedPromotion
{
    private DateTime _lastModified = DateTime.MinValue;
    public string _filePath { get; set; }
    public IPromotionStrategy _promotionStrategy { get; private set; }
    public bool IsEnabled { get; set; } = true;

    //PRE: filePath is a valid path to a file
    public void Verify()
    {
        var currentLastModifed = File.GetLastWriteTime(_filePath);
        if (_lastModified != currentLastModifed)
        {
            _lastModified = currentLastModifed;
            Update();
        }
    }
    
    private void Update()
    {
        var assembly = Assembly.LoadFrom(_filePath);
        var interfaceType = typeof(IPromotionStrategy);
        var type = assembly.GetTypes().FirstOrDefault(t => t.IsClass && interfaceType.IsAssignableFrom(t));
        if (type != null)
        {
            _promotionStrategy = (IPromotionStrategy)Activator.CreateInstance(type)!;
        }
        else
        {
            throw new NotSupportedException("The file does not contain a valid promotion strategy");
        }
    }
}