namespace Wba.EFbasics.Web.ViewModels
{
    public class HorsesInfoViewModel : BaseViewModel
    {
        public string Country { get; set; }
        public BaseViewModel Race { get; set; }
        public string DateOfBirth { get; set; }
        public string IdentificationCode { get; set; }
        public decimal Weight { get; set; }
    }
}
