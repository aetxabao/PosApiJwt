
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PosApiJwt.Models
{
    public class MsgBody
    {
        [JsonIgnore]
        [ForeignKey("Message")]
        public int MsgBodyId { get; set; }

        public string Msg { get; set; }
        public string Stamp { get; set; }

        [JsonIgnore]
        public virtual Message Message { get; set; }
    }

}
