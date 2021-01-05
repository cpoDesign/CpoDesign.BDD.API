namespace CpoDesign.BDD.API.CpoDesign
{


    public class Postmanv3template
    {
        public Info info { get; set; }
        public Item[] item { get; set; }
    }

    public class Info
    {
        public string _postman_id { get; set; }
        public string name { get; set; }
        public string schema { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public Event[] _event { get; set; }
        public Request request { get; set; }
        public object[] response { get; set; }
    }

    public class Request
    {
        public string method { get; set; }
        public Header[] header { get; set; }
        public Url url { get; set; }
    }

    public class Url
    {
        public string raw { get; set; }
        public string[] host { get; set; }
    }

    public class Header
    {
        public string key { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }

    public class Event
    {
        public string listen { get; set; }
        public Script script { get; set; }
    }

    public class Script
    {
        public string[] exec { get; set; }
        public string type { get; set; }
    }

}
