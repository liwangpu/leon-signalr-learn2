using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gateway.API.JsonMerge
{
    public class JsonMergeSource : JsonConfigurationSource
    {
        private readonly JsonMergeOptions _options;
        public IEnumerable<IFileInfo> Files { get; private set; }

        public JsonMergeSource(JsonMergeOptions options)
        {
            this._options = options;
        }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var fileProvider = builder.GetFileProvider();
            this.Files = this._options.Files
                                      .Select(x => fileProvider.GetFileInfo(x))
                                      .Where(x => x.Exists ? true : throw new InvalidOperationException($"file not exist :{x.PhysicalPath}"));

            return new JsonMergeProvider(this);
        }
    }
}
