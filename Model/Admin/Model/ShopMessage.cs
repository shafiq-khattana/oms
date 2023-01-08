using System;
using System.Collections.Generic;

namespace Model.Admin.Model
{
    public class ShopMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

        public List<AppMessageState> MessageStates { get; set; }

        public ShopMessage()
        {
            MessageStates = new List<AppMessageState>();
        }
    }
}