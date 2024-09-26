using Newtonsoft.Json.Schema;
using System;
using System.IO;

namespace RestAPISampleAutoTests.Utils
{
    public static class JsonSchemas
    {
        private static readonly string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Schemas");

        public static JSchema UsersJsonSchema()
        {
            return LoadSchemaFromFile("users-schema.json");
        }

        private static JSchema LoadSchemaFromFile(string fileName)
        {
            string filePath = Path.Combine(BaseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Schema file '{fileName}' not found at path: {filePath}");
            }

            string jsonSchema = File.ReadAllText(filePath);
            JSchema schema = JSchema.Parse(jsonSchema);
            return schema;
        }
    }
}
