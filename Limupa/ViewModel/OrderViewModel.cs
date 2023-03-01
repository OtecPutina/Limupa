namespace Limupa.ViewModel
{
    public class OrderViewModel
    {
        public List<CheckoutItemViewModel> CheckoutItemViewModels  { get; set; }
        public string Fullname { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string? Note { get; set; }
    }
}
