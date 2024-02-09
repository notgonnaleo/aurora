namespace Frontend.Web.Components.Layout.Accordion
{
    public class AccordionItem
    {
        public int Id { get; set; }
        public Guid ItemId { get; set; }
        public string Title { get; set; }
        public List<AccordionSubItem>? Body { get; set; }
        public bool IsCollapsed { get; set; } = true;

        public AccordionItem() { }

        public AccordionItem(int itemSequence, Guid itemId, string itemName, List<AccordionSubItem>? itemBody)
        {
            Id = itemSequence;
            ItemId = itemId;
            Title = itemName;
            Body = itemBody;
            IsCollapsed = true;
        }
    }

    public class AccordionSubItem
    {
        public AccordionSubItem() { }

        public Guid SubItemId { get; set; }
        public string SubTitle { get; set; }

        public AccordionSubItem(Guid subItemId, string subTitle)
        {
            SubItemId = subItemId;
            SubTitle = subTitle;
        }
    }
}
