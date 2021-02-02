namespace HareDu.Core.Serialization
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomListConverter :
        JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => throw new NotImplementedException();

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    }
}