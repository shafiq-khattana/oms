using Model.Financials.Model;

namespace Model.ReadyStuff.Model
{
    public class ReadyItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public decimal StockQty { get; set; }

        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
    }
}