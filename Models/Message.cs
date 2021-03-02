using System.Text.Json.Serialization;

namespace PosApiJwt.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string To { get; set; }
        public string From { get; set; }

        public virtual MsgBody MsgBody { get; set; }

        public void Copy(Message m)
        {
            if (m != null)
            {
                this.To = m.To;
                this.From = m.From;
                if (this.MsgBody == null && m.MsgBody != null)
                {
                    this.MsgBody = new MsgBody();
                }
                if (m.MsgBody == null)
                {
                    this.MsgBody = null;
                }
                else
                {
                    this.MsgBody.Msg = m.MsgBody.Msg;
                    this.MsgBody.Stamp = m.MsgBody.Stamp;
                }
            }
        }
    }
}
