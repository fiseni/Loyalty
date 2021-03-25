using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.ApiGateway.Configuration
{
    public class ApplicationOptions
    {
        public const string CONFIG_NAME = "ApplicationOptions";

        public static ApplicationOptions Instance { get; } = new ApplicationOptions();
        private ApplicationOptions() { }

        public string Type { get; set; }

        public ApplicationType ApplicationType => Type.Equals("Monolithic", StringComparison.CurrentCultureIgnoreCase) ? ApplicationType.Monolithic : ApplicationType.Distributed;
    }

    public enum ApplicationType
    {
        Monolithic = 1,
        Distributed = 2
    }
}
