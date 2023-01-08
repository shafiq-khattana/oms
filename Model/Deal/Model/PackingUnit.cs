using System.Collections.Generic;

namespace Model.Deal.Model
{
    public class PackingUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<OmeDeal> Deals { get; set; }

        public PackingUnit()
        {
            Deals = new List<OmeDeal>();
        }
    }
}