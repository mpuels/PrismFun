namespace PrismFun.ViewModels
{
    public class Telefonbucheintrag
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Telefonnummer { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}",
                                 Vorname, Nachname, Telefonnummer);
        }
    }
}
