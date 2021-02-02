namespace HareDu.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Extensions;
    using Model;

    public static class ValueExtensions
    {
        public static IDictionary<string, object> GetArguments(this IDictionary<string, ArgumentValue<object>> arguments)
        {
            if (arguments.IsNull() || !arguments.Any())
                return new Dictionary<string, object>();
            
            return arguments.ToDictionary(x => x.Key, x => x.Value.Value);
        }
        
        public static IDictionary<string, string> GetStringArguments(this IDictionary<string, ArgumentValue<object>> arguments)
        {
            if (arguments.IsNull() || !arguments.Any())
                return new Dictionary<string, string>();
            
            return arguments.ToDictionary(x => x.Key, x => x.Value.Value.ToString());
        }
    }
}