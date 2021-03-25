using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.ApiGateway.Configuration
{
    public class ActionHidingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (action.DisplayName.Contains("CustomerApp.Api"))
            {
                action.ApiExplorer.IsVisible = false;
            }
        }
    }
}
