using System.Collections.Generic;

namespace Gateway.API.JsonMerge
{
    public class JsonMergeOptions
    {
        public List<string> Files = new List<string>();

        public JsonMergeOptions AddJsonFile(string path)
        {
            this.Files.Add(path);
            return this;
        }
    }
}
