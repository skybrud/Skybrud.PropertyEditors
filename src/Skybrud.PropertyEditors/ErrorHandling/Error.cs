using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Skybrud.PropertyEditors.ErrorHandling {

    public class Error {

        [JsonProperty("id")]
        public string Id { get; private set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonIgnore]
        public string Url { get; private set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; private set; }

        [JsonIgnore]
        public Dictionary<string, object> Arguments { get; private set; }

        public Error(string message) : this(message, new Dictionary<string, object>()) { }

        public Error(string message, Dictionary<string, object> arguments) {
            Id = Guid.NewGuid().ToString();
            Message = message;
            Url = HttpContext.Current == null ? null : HttpContext.Current.Request.Url.ToString();
            Timestamp = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
            Arguments = arguments ?? new Dictionary<string, object>();
        }

        public override string ToString() {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Message);
            sb.AppendLine();
            sb.AppendLine("ID:   " + Id);
            sb.AppendLine("Url:  " + (Url ?? "N/A"));

            if (Arguments.Count > 0) {
                sb.AppendLine("Arguments:");
                foreach (var pair in Arguments) {
                    sb.AppendLine("    " + pair.Key + " => " + pair.Value);
                }
            }

            return sb.ToString();

        }

    }

}