using Newtonsoft.Json;

namespace SM.Common.Serialization
{
	class SerializationUtil
	{
		public static TType Deserialize<TType>(string data)
		{
			return JsonConvert.DeserializeObject<TType>(data);
		}

		public static string Serialize(object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}