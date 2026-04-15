using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class QuaternionConvert : JsonConverter<Quaternion>
{
    public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
  
        Quaternion q = Quaternion.identity;
        JObject jobj = JObject.Load(reader);
        q.x = (float)jobj["X"];
        q.y = (float)jobj["Y"];
        q.z = (float)jobj["Z"];
        q.w = (float)jobj["W"];
        return q;

    }

    public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("X");
        writer.WriteValue(value.x);
        writer.WritePropertyName("Y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("Z");
        writer.WriteValue(value.z);
        writer.WritePropertyName("W");
        writer.WriteValue(value.w);
        writer.WriteEndObject();
    }
}
