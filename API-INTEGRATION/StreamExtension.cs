namespace APIIntegartion
{
    public static class StreamExtensions
    { 
        public static T ReadAndDeserializeFromJson<T>(this Stream stream)
        {
            if(stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (!stream.CanRead)
            {
                throw new NotSupportedException("Cannot read from this stream");
            }
            using (var streamReader = new StreamReader(stream))
            {
                using(var jsonReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                    return jsonSerializer.Deserialize<T>(jsonReader);
                }
            }
        }
    }
}