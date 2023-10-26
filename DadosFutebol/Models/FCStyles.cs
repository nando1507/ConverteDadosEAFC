namespace DadosFutebol.Models
{
    public class FCStyles
    {
        public string StyleName { get; set; }
        public string StyleURLImg { get; set; }

        public override string ToString()
        {
            return $@"{nameof(StyleName)} : {StyleName}; {nameof(StyleURLImg)} : {StyleURLImg}";
        }
    }
}
