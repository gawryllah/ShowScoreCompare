namespace ShowScoreCompare.Data
{
    public class ImdbIdGetter
    {


        public class ImdbModel
        {
            public string searchType { get; set; }
            public string expression { get; set; }
            public ImdbInnerInfo[] results { get; set; }
            public string errorMessage { get; set; }
        }

        public class ImdbInnerInfo
        {
            public string id { get; set; }
            public string resultType { get; set; }
            public string image { get; set; }
            public string title { get; set; }
            public string description { get; set; }
        }
    }
}
