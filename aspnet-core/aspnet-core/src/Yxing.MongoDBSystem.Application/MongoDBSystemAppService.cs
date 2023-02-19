using System;
using System.Collections.Generic;
using System.Text;
using Yxing.MongoDBSystem.Localization;
using Volo.Abp.Application.Services;

namespace Yxing.MongoDBSystem;

/* Inherit your application services from this class.
 */
public abstract class MongoDBSystemAppService : ApplicationService
{
    protected MongoDBSystemAppService()
    {
        LocalizationResource = typeof(MongoDBSystemResource);
    }
}
