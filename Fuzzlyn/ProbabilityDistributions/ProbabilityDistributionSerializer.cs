using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fuzzlyn.ProbabilityDistributions
{
    internal class ProbabilityDistributionSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ProbabilityDistribution);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType != typeof(ProbabilityDistribution))
            {
                // Avoid infinite recursion...
                serializer.ContractResolver.ResolveContract(objectType).Converter = null;
                return serializer.Deserialize(reader, objectType);
            }

            JObject obj = JObject.Load(reader);
            switch (obj["Type"].Value<string>())
            {
                case nameof(GeometricDistribution):
                    return obj.ToObject<GeometricDistribution>(serializer);
                case nameof(UniformRangeDistribution):
                    return obj.ToObject<UniformRangeDistribution>(serializer);
                case nameof(TableDistribution):
                    return obj.ToObject<TableDistribution>(serializer);
                default:
                    throw new NotSupportedException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override bool CanWrite => false;
    }
}
