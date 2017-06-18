using SM.Common.Serialization;
using System.IO;

namespace SM.Common.Security
{
	/// <summary>
	/// This class is used to separate the "secret" keys into a file that should NOT be checked into your Git repository.
	/// The basic structure is contained within the OAuthTemplate.json file.
	/// Anything prefixed with "Secret" is will .git ignored.
	/// </summary>
	public class OAuth
	{
		public string AccessToken { get; set; }

		public string AccessTokenSecret { get; set; }

		public string CallbackUri { get; set; }

		public string ConsumerKey { get; set; }

		public string ConsumerKeySecret { get; set; }

		public void Load(string fileName = "Secret.json")
		{
			var file = File.OpenRead(fileName);
			var reader = new StreamReader(file);
			var data = reader.ReadToEnd();

			var loaded = SerializationUtil.Deserialize<OAuth>(data);

			Copy(loaded);
		}

		private void Copy(OAuth toCopy)
		{
			AccessToken = toCopy.AccessToken;
			AccessTokenSecret = toCopy.AccessTokenSecret;
			CallbackUri = toCopy.CallbackUri;
			ConsumerKey = toCopy.ConsumerKey;
			ConsumerKeySecret = toCopy.ConsumerKeySecret;
		}
	}
}