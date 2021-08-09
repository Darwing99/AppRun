using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRun.Model
{
    class ModelCountry
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("topLevelDomain")]
        public List<string> topLevelDomain { get; set; }
        [JsonProperty("alpha2Code")]
        public string alpha2Code { get; set; }
        [JsonProperty("alpha3Code")]
        public string alpha3Code { get; set; }
        public List<string> callingCodes { get; set; }
        [JsonProperty("capital")]
        public string capital { get; set; }
        [JsonProperty("altSpellings")]
        public List<string> altSpellings { get; set; }
        [JsonProperty("region")]
        public string region { get; set; }
        [JsonProperty("subregion")]
        public string subregion { get; set; }
        [JsonProperty("population")]
        public int population { get; set; }
        [JsonProperty("latlng")]
        public List<double> latlng { get; set; }
        [JsonProperty("demonym")]
        public string demonym { get; set; }
        [JsonProperty("area")]
        public double? area { get; set; }
        [JsonProperty("gini")]
        public double? gini { get; set; }
        [JsonProperty("timezones")]
        public List<string> timezones { get; set; }
        [JsonProperty("borders")]
        public List<string> borders { get; set; }
        [JsonProperty("nativeName")]
        public string nativeName { get; set; }
        [JsonProperty("numericCode")]
        public string numericCode { get; set; }
        [JsonProperty("currencies")]
        public List<Currency> currencies { get; set; }
        [JsonProperty("languages")]
        public List<Language> languages { get; set; }
        [JsonProperty("translations")]
        public Translations translations { get; set; }
        [JsonProperty("flag")]
        public string flag { get; set; }
        [JsonProperty("regionalBlocs")]
        public List<RegionalBloc> regionalBlocs { get; set; }
        [JsonProperty("cioc")]
        public string cioc { get; set; }

        public class Currency
        {
            [JsonProperty("code")]
            public string code { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("symbol")]
            public string symbol { get; set; }
        }

        public class Language
        {
            [JsonProperty("iso639_1")]
            public string iso639_1 { get; set; }
            [JsonProperty("iso639_2")]
            public string iso639_2 { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("nativeName")]
            public string nativeName { get; set; }
        }

        public class Translations
        {
            [JsonProperty("de")]
            public string de { get; set; }
            [JsonProperty("es")]
            public string es { get; set; }
            [JsonProperty("fr")]
            public string fr { get; set; }
            [JsonProperty("ja")]
            public string ja { get; set; }
            [JsonProperty("it")]
            public string it { get; set; }
            [JsonProperty("br")]
            public string br { get; set; }
            [JsonProperty("pt")]
            public string pt { get; set; }
            [JsonProperty("nl")]
            public string nl { get; set; }
            [JsonProperty("hr")]
            public string hr { get; set; }
            [JsonProperty("fa")]
            public string fa { get; set; }
        }

        public class RegionalBloc
        {
            [JsonProperty("acronym")]
            public string acronym { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("otherAcronyms")]
            public List<string> otherAcronyms { get; set; }
            [JsonProperty("otherNames")]
            public List<object> otherNames { get; set; }
        }



    }
}
