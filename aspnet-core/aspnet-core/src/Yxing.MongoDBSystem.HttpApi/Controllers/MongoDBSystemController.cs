using Yxing.MongoDBSystem.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Yxing.MongoDBSystem.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MongoDBSystemController : AbpControllerBase
{
    protected MongoDBSystemController()
    {
        LocalizationResource = typeof(MongoDBSystemResource);
    }
}
