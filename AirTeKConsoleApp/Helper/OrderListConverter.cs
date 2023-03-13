using AirTeKConsoleApp.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AirTeKConsoleApp.Helper
{
    public class OrderListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<Order>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var orders = new List<Order>();

            // Load the JSON into a JObject so we can enumerate over the properties
            var jObject = JObject.Load(reader);

            // Loop through each property and deserialize it into an Order object
            foreach (var property in jObject.Properties())
            {
                var order = new Order
                {
                    Id = property.Name,
                    Destination = property.Value["destination"].Value<string>()
                };

                orders.Add(order);
            }

            return orders;
        }

        //Note: Not implemented because we only need to deserialize
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
