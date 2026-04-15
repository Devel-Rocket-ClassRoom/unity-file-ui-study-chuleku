using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class ColorConvert : JsonConverter<Color>
{
    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Color c = default;
        JObject jobj = JObject.Load(reader);
        c.r = (float)jobj["R"];
        c.g = (float)jobj["G"];
        c.b = (float)jobj["B"];
        c.a = (float)jobj["A"];
        return c;
    }

    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("R");
        writer.WriteValue(value.r);
        writer.WritePropertyName("G");
        writer.WriteValue(value.g);
        writer.WritePropertyName("B");
        writer.WriteValue(value.b);
        writer.WritePropertyName("A");
        writer.WriteValue(value.a);
        writer.WriteEndObject();
    }
}
