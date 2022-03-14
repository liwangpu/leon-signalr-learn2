using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gateway.API.JsonMerge
{
    public class JsonMergeProvider : JsonConfigurationProvider
    {
        private readonly IEnumerable<IFileInfo> _files;
        public JsonMergeProvider(JsonMergeSource source) : base(source)
        {
            this._files = source.Files;
        }

        public override void Load()
        {
            var objs = this._files
                           .Select(x => File.ReadAllText(x.PhysicalPath))
                           .Select(text => JsonConvert.DeserializeObject(text));

            var merged = new JObject();

            foreach (var item in objs)
            {
                merged.Merge(item);
            }

            var content = JsonConvert.SerializeObject(merged);
            Console.WriteLine(content);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                this.Load(stream);
            }
        }
    }
}

