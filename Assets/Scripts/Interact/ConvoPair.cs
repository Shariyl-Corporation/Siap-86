using System.Collections.Generic;

public class Convo {
    public string speaker;
    public string text;
}

public static class ConvoPair {
    public static List<List<Convo>> ConvGreetings;
    public static List<List<Convo>> ConvKTP;
    public static List<List<Convo>> ConvNoKTP;
    public static List<List<Convo>> ConvSIM;
    public static List<List<Convo>> ConvNoSIM;
    public static List<List<Convo>> ConvSTNK;
    public static List<List<Convo>> ConvNoSTNK;
    public static List<List<Convo>> ConvVerdictGuilty;
    public static List<List<Convo>> ConvVerdictInnocent;
    
    public static void Init() {
        string you = "You";
        string driver = "???";

        ConvGreetings = new (){
            new() {
                new() { speaker = you, text = "" },
                new() { speaker = driver, text = ""}
            }
        };
        ConvKTP = new List<List<Convo>>();
        ConvNoKTP = new List<List<Convo>>();
        ConvSIM = new List<List<Convo>>();
        ConvNoSIM = new List<List<Convo>>();
        ConvSTNK = new List<List<Convo>>();
        ConvNoSTNK = new List<List<Convo>>();
        ConvVerdictGuilty = new List<List<Convo>>();
        ConvVerdictInnocent = new List<List<Convo>>();


        // ConvGreetings
        List<Convo> conv = new()
        {
            new() { speaker = you, text = "" },
            new() { speaker = driver, text = ""}
        };
        ConvGreetings.Add(conv);

    }    
}