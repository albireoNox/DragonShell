using System.IO;
using YamlDotNet.Serialization;

namespace main_cli.io.file
{
    static class YamlFile
    {
        public static T deserializeYamlFile<T>(FileInfo file)
        {
            var deserializer = new DeserializerBuilder()
                .Build();
            return deserializer.Deserialize<T>(file.OpenText());
        }
    }
}
