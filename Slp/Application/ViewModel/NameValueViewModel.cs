namespace Application.ViewModel
{
    public class NameValueViewModel
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public bool SelectedItem { get; set; }


        public string NameWithoutBlanks { get { return Name.Replace(" ", ""); } }

        public string Type { get; set; }

        public string cboxId {
            get { return "cb" + Value; }
        }
    }
}
