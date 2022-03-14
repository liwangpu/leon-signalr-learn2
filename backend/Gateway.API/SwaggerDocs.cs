using System.Collections.Generic;

namespace Gateway.API
{
    public class SwaggerDocs
    {
        public List<SwaggerDoc> Items { get; set; }
    }

    public class SwaggerDoc
    {
        public string Name { get; set; }
        public string Endpoint { get; set; }
    }
}
