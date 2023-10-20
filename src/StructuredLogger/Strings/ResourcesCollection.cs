using System.Collections.Generic;
using System.IO;
using ResourcesDictionary = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;
#if NET7_0_OR_GREATER
using System.Text.Json.Serialization;
using System.Text.Json;
#endif

namespace Microsoft.Build.Logging.StructuredLogger
{

#if NET7_0_OR_GREATER
    [JsonSerializable(typeof(ResourcesDictionary), TypeInfoPropertyName = "ResourcesDictionary")]
    partial class ResourcesDictionaryContext : JsonSerializerContext
    {

    }
#endif

    public partial class StringsSet
    {
        private Dictionary<string, string> currentSet;
        public string Culture { get; set; }

        public StringsSet(string culture)
        {
            Culture = culture;
            currentSet = ResourcesCollection[culture];
        }

        private static ResourcesDictionary resourcesCollection;
        public static ResourcesDictionary ResourcesCollection
        {
            get
            {
                if (resourcesCollection == null)
                {
                    var assembly = typeof(StructuredLogger).Assembly;
                    var stream = assembly.GetManifestResourceStream(@"Strings.json");

                    var reader = new StreamReader(stream);
                    var text = reader.ReadToEnd();
#if NET7_0_OR_GREATER
                    resourcesCollection = JsonSerializer.Deserialize<ResourcesDictionary>(text, ResourcesDictionaryContext.Default.ResourcesDictionary);
#else
                    resourcesCollection = TinyJson.JSONParser.FromJson<ResourcesDictionary>(text);
#endif
                }

                return resourcesCollection;
            }
        }

        public string GetString(string key)
        {
            if (currentSet.TryGetValue(key, out var value))
            {
                return value;
            }

            return string.Empty;
        }
    }
}
